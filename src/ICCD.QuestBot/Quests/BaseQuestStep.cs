using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Quests;

/// <summary>
/// The quest step base class.
/// </summary>
public abstract class BaseQuestStep : IQuestStep
{
    private static List<Type>? _allQuestTriggerTypes = null;
    private ICollection<IQuestTrigger> _triggers = new List<IQuestTrigger>();

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseQuestStep"/> class.
    /// </summary>
    /// <param name="parentQuest">The parent quest.</param>
    /// <param name="context">The quest context.</param>
    protected BaseQuestStep(IQuest parentQuest, QuestContext context)
    {
        ParentQuest = parentQuest;
        Context = context;
        Status = IQuestStep.StepStatus.Initialised;
    }

    /// <inheritdoc />
    public abstract string Title { get; }

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public IQuestStep.StepStatus Status { get; set; }

    /// <inheritdoc />
    public IMessage CurrentMessage { get; private set; }

    /// <inheritdoc />
    public IQuest ParentQuest { get; }

    /// <inheritdoc />
    public QuestContext Context { get; }

    /// <inheritdoc />
    public ICollection<IQuestTrigger> Triggers => _triggers;

    /// <summary>
    /// Gets or sets a value indicating whether the step is completed.
    /// </summary>
    public bool StepCompleted { get; set; }

    /// <inheritdoc />
    public virtual async Task Execute()
    {
        try
        {
            if (ParentQuest.QuestStatus.Code == StatusCode.Failed ||
                ParentQuest.QuestStatus.Code == StatusCode.Succeeded)
            {
                return;
            }

            _allQuestTriggerTypes ??= typeof(IQuestTrigger<>).Assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && t.GetInterfaces().Any(x =>
                    x.IsGenericType && (x.GetGenericTypeDefinition() == typeof(IQuestTrigger<>))))
                .ToList();

            var allTriggers =
                _allQuestTriggerTypes.Where(x =>
                    x.BaseType.IsGenericType && (x.BaseType.GetGenericArguments()![0] == this.GetType() ||
                                                 x.BaseType.GetGenericArguments()![0] == this.GetType()!.BaseType));

            // Invoke all triggers.
            // Set Context and Initialise.
            var allExecuteTasks = new List<Task>();
            foreach (var trigger in allTriggers)
            {
                var instance = (IQuestTrigger)Activator.CreateInstance(trigger);
                _triggers.Add(instance);
            }

            _triggers = _triggers.OrderBy(x => x.OrderSequence).ThenBy(x => x.Name).ToList();

            // Check if the channel we are supposed to work in exist as MessageChannel.
            if (!string.IsNullOrEmpty(Title) || !string.IsNullOrEmpty(Description))
            {
                var eb = new EmbedBuilder().WithCurrentTimestamp();
                if (!string.IsNullOrEmpty(Title))
                {
                    eb = eb.WithTitle(ParentQuest.ReplaceVariables(Title));
                }

                if (!string.IsNullOrEmpty(Description))
                {
                    eb = eb.WithDescription(ParentQuest.ReplaceVariables(Description));
                }

                eb = PreMessageSend(eb);
                foreach (var instance in _triggers)
                {
                    eb = instance.TransformStepMessage(eb);
                }

                CurrentMessage = await ParentQuest.SendMessage(embed: eb.Build());
                eb.WithFooter(CurrentMessage.Id.ToString());
                await ((RestUserMessage)CurrentMessage).ModifyAsync(x => x.Embed = eb.Build());
            }

            foreach (var instance in _triggers)
            {
                var mi = instance.GetType().GetMethod("Init");
                var t = (Task)mi.Invoke(instance, new object[] { this, Context });
                await t;
                if (instance.Skip)
                {
                    continue;
                }

                instance.Status = IQuestTrigger.TriggerStatus.Initialised;
                mi = instance.GetType().GetMethod("Run");
                t = (Task)mi.Invoke(instance, new object[] { });
                if (instance.Skip)
                {
                    instance.Status = IQuestTrigger.TriggerStatus.Completed;
                    continue;
                }

                instance.Status = IQuestTrigger.TriggerStatus.Running;
                allExecuteTasks.Add(t);
            }

            Task.WaitAll(allExecuteTasks.ToArray());
            Console.WriteLine($"{this.ParentQuest.ToString()} - {this.GetType().Name} - All triggers executed!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{this.ParentQuest.ToString()} - {this.GetType().Name} - ERROR: {ex.ToString()}");
        }
        finally
        {
            foreach (var trigger in _triggers)
            {
                if (trigger.Status != IQuestTrigger.TriggerStatus.Triggered)
                {
                    trigger.Status = IQuestTrigger.TriggerStatus.Completed;
                }
            }
            Status = IQuestStep.StepStatus.Completed;
            Console.WriteLine($"{this.ParentQuest.ToString()} - {this.GetType().Name} - Step complete!");
        }
    }

    public virtual EmbedBuilder PreMessageSend(EmbedBuilder eb)
    {
        return eb;
    }
}