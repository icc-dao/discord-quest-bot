using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Quests;

public abstract class BaseQuestTrigger<TQuestStep> : IQuestTrigger<TQuestStep>
    where TQuestStep : BaseQuestStep, IQuestStep
{
    private IQuestStep _nextStep;

    public abstract string Name { get; }
    public bool Skip { get; set; }
    public IQuestTrigger.TriggerStatus Status { get; set; }
    public QuestContext Context { get; set; }
    public TQuestStep ParentStep { get; set; }

    public abstract Type? NextStepType { get; }

    public abstract QuestActionType Type { get; }
    public abstract QuestTriggerResult TriggerResult { get; }
    public abstract uint OrderSequence { get; }

    public ICollection<IQuestTriggerAction> QuestTriggerActions { get; } = new List<IQuestTriggerAction>();

    public virtual async Task Init(TQuestStep parentStep, QuestContext context)
    {
        ParentStep = parentStep;
        Context = context;

        if (NextStepType != null)
        {
            if (!NextStepType.IsSubclassOf(typeof(BaseQuestStep)))
            {
                throw new InvalidOperationException(
                    $"Invalid type {NextStepType.FullName} for parameter {nameof(NextStepType)}, has to implement base class {nameof(BaseQuestStep)}.");
            }

            _nextStep = Activator.CreateInstance(NextStepType, this.ParentStep.ParentQuest, Context) as IQuestStep;
        }
    }

    public virtual void Trigger()
    {
        Console.WriteLine(
            $"{this.ParentStep.ParentQuest.ToString()} - {this.ParentStep.GetType().Name} - {this.Name}: Trigger!");
        if (ParentStep.StepCompleted)
        {
            return;
        }

        switch (this.TriggerResult)
        {
            case QuestTriggerResult.Failure:
                if (this.ParentStep.ParentQuest.QuestStatus.Code != StatusCode.Failed)
                {
                    this.ParentStep.ParentQuest.QuestStatus.Fail();
                }

                break;
            case QuestTriggerResult.Success:
                if (this.ParentStep.ParentQuest.QuestStatus.Code != StatusCode.Succeeded)
                {
                    this.ParentStep.ParentQuest.QuestStatus.Success();
                }

                break;
        }

        if (_nextStep != null || TriggerResult != QuestTriggerResult.Continue)
        {
            if (_nextStep != null)
            {
                this.ParentStep.ParentQuest.QuestSteps.Add(_nextStep);
                this.ParentStep.ParentQuest.ExecutingQuestSteps.Add(_nextStep.Execute());
            }

            ParentStep.StepCompleted = true;
        }

        Status = IQuestTrigger.TriggerStatus.Triggered;
    }

    public void Untrigger()
    {
        Console.WriteLine(
            $"{this.ParentStep.ParentQuest.ToString()} - {this.ParentStep.GetType().Name} - {this.Name}: Untrigger!");
        if (ParentStep.StepCompleted)
        {
            return;
        }
        if (Status == IQuestTrigger.TriggerStatus.Triggered)
        {
            Status = IQuestTrigger.TriggerStatus.Running;
        }
    }

    public virtual Task Run()
    {
        return Task.Run(() =>
        {
            while (true)
            {
                if (ParentStep.StepCompleted)
                {
                    break;
                }

                Thread.Sleep(30);
            }

            Console.WriteLine(
                $"{this.ParentStep.ParentQuest.ToString()} - {this.ParentStep.GetType().Name} - {this.Name}: Trigger complete!");
        });
    }

    public virtual async Task Action()
    {
        if (QuestTriggerActions != null)
        {
            foreach (var questTriggerAction in QuestTriggerActions)
            {
                questTriggerAction.ExecuteUp(Context);
            }
        }
    }

    public virtual EmbedBuilder TransformStepMessage(EmbedBuilder eb)
    {
        return eb;
    }

    public void AddTriggerAction(IQuestTriggerAction action)
    {
        QuestTriggerActions.Add(action);
    }
}