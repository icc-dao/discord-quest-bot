using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using ICCD.QuestBot.Quests;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.Interfaces;
using ICCD.QuestBot.Quests.TriggerAction;
using ICCD.QuestBot.Quests.Triggers;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;

public class TriggerPurposeLurker : QuestReactionTrigger<Step3PurposeBase>
{
    private Type _nextStepType;
    private QuestTriggerResult _triggerResult;

    public TriggerPurposeLurker()
    {
        AddTriggerAction(new AddRoleTriggerAction("Lurker"));
    }

    public override string Name => "Lurker";

    public override Type NextStepType => _nextStepType;

    public override QuestTriggerResult TriggerResult => _triggerResult;
    public override uint OrderSequence { get; }
    public override Task Init(Step3PurposeBase parentStep, QuestContext context)
    {
        if (parentStep.ParentQuest.QuestSteps.SelectMany(x => x.Triggers.Where(x => x.Status == IQuestTrigger.TriggerStatus.Triggered)).Any(x => x.GetType() == this.GetType()))
        {
            Skip = true;
            return Task.CompletedTask;
        }

        _nextStepType = typeof(Step3SecondaryPurpose);
        if (parentStep.ParentQuest.QuestSteps.Any(x => x.Status == IQuestStep.StepStatus.Completed && x.GetType().BaseType == typeof(Step3PurposeBase)))
        {
            _triggerResult = QuestTriggerResult.Success;
        }

        return base.Init(parentStep, context);
    }

    public override IEmote ReactionEmote { get; } = Emoji.Parse(":eyes:");
}