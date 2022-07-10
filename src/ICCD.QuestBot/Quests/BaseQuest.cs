using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using ICCD.QuestBot.Data.Quests;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Quests;

/// <summary>
/// Abstract base implementation of the <see cref="IQuest"/> interface.
/// </summary>
public abstract class BaseQuest : IQuest
{
    private readonly Status _questStatus = new();
    private readonly QuestConfigurationManager.QuestConfiguration _questConfiguration;
    private bool _sendMessageFailed = false;
    private List<Task> _executingQuestSteps = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseQuest"/> class.
    /// </summary>
    /// <param name="context">The quest context.</param>
    protected BaseQuest(QuestContext context)
    {
        Context = context;
        _questConfiguration = QuestConfigurationManager.Instance.Get(GetType());
    }

    /// <inheritdoc />
    public abstract string Id { get; }

    /// <inheritdoc />
    public QuestContext Context { get; }

    /// <inheritdoc />
    public abstract string Title { get; }

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public abstract Type StartingStepType { get; }

    /// <inheritdoc />
    public Status QuestStatus => _questStatus;

    /// <inheritdoc />
    public List<Task> ExecutingQuestSteps => _executingQuestSteps;

    /// <inheritdoc />
    public ICollection<IQuestStep> QuestSteps { get; } = new List<IQuestStep>();

    /// <inheritdoc />
    public async Task<IMessage> SendMessage(string? message = null, Embed? embed = null)
    {
        if (!_sendMessageFailed)
        {
            switch (_questConfiguration.MessageContext.MessageChannel)
            {
                case QuestConfigurationManager.QuestConfiguration.QuestMessageContext.QuestMessageChannel.DM:
                    try
                    {
                        var dmChannel = await Context.SocketGuildUser.CreateDMChannelAsync();
                        return await dmChannel.SendMessageAsync(message, embed: embed);
                    }
                    catch (Exception ex)
                    {
                        _sendMessageFailed = true;
                        await SendStatus($@"GM <@{Context.SocketGuildUser.Id}> we wanted to send you on a quest but could not send you a DM.
To be able to use this Discord server make sure that you accept direct messages from other server members.
After you have enabled DMs type ``!quest restart {_questConfiguration.QuestId}`` to retry!
More info: https://support.discord.com/hc/en-us/articles/217916488-Blocking-Privacy-Settings-");
                        throw;
                    }
            }
        }

        throw new ApplicationException("Couldn't send message.");
    }

    public async Task SendStatus(string? message = null, Embed? embed = null, ulong cleanupTimer = 60000)
    {
        var cleanup = cleanupTimer <= 0;
        if (Context.SocketGuildUser.Guild.GetChannel(_questConfiguration.MessageContext.StatusMessageChannelId ?? 0) is not
            IMessageChannel messageChannel)
        {
            return;
        }

        var statusMessage = await messageChannel.SendMessageAsync(message, embed: embed);
        if (cleanup)
        {
            Task.Delay(TimeSpan.FromMilliseconds(cleanupTimer)).ContinueWith(async task => { await messageChannel.DeleteMessageAsync(statusMessage); });
        }
    }

    public virtual async Task Finish()
    {
        try
        {
            if (this.QuestStatus.Code == StatusCode.Succeeded)
            {
                var role = Context.SocketGuildUser.Guild.GetRole(_questConfiguration.QuestCompletionRoleId);
                if (role != null)
                {
                    await Context.SocketGuildUser.AddRoleAsync(role.Id);
                }
            }
        }
        catch (Exception ex)
        {
            await SendStatus(ex.Message);
        }

        await SendMessage(embed: _questStatus.ToEmbed(this));
    }

    public virtual async Task ResetAction()
    {
        var role = Context.SocketGuildUser.Guild.GetRole(_questConfiguration.QuestCompletionRoleId);
        if (role != null)
        {
            await Context.SocketGuildUser.RemoveRoleAsync(role);
        }
    }

    public string ReplaceVariables(string text)
    {
        return text.Replace("@{user}", $"<@{Context.SocketGuildUser.Id}>");
    }

    public async Task Execute()
    {
        _questStatus.SetStatus(StatusCode.Running);
        try
        {
            if (
                (!string.IsNullOrEmpty(Title) || !string.IsNullOrEmpty(Description)))
            {
                var eb = new EmbedBuilder().WithCurrentTimestamp();
                if (!string.IsNullOrEmpty(Title))
                {
                    eb = eb.WithTitle(ReplaceVariables(Title));
                }

                if (!string.IsNullOrEmpty(Description))
                {
                    eb = eb.WithDescription(ReplaceVariables(Description));
                }

                await SendMessage(embed: eb.Build());
            }

            var instance = Activator.CreateInstance(StartingStepType, this, Context) as IQuestStep;
            instance.Status = IQuestStep.StepStatus.Initialised;
            QuestSteps.Add(instance);
            _executingQuestSteps.Add(instance.Execute());
            instance.Status = IQuestStep.StepStatus.Running;
            while (_executingQuestSteps.Count > 0)
            {
                await Task.WhenAll(_executingQuestSteps);
                _executingQuestSteps = _executingQuestSteps.Where(t => !t.IsCompleted).ToList();
            }

            Console.WriteLine($"{this.ToString()}: All steps complete.");

            foreach (var questStep in QuestSteps)
            {
                foreach (var trigger in questStep.Triggers)
                {
                    if (trigger.Status == IQuestTrigger.TriggerStatus.Triggered)
                    {
                        await trigger.Action();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{this.ToString()}: Error {ex.ToString()}.");
            _questStatus.Fail(reason: ex.Message);
        }
        finally
        {
            Console.WriteLine($"{this.ToString()}: Completed.");
            await Finish();
        }
    }

    public override string ToString()
    {
        return $"{this._questConfiguration.QuestId}:{this.Context.SocketGuildUser.Username}";
    }
}