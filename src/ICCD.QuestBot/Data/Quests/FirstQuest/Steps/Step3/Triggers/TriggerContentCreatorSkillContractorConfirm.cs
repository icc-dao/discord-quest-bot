using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using ICCD.QuestBot.Quests;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.Interfaces;
using ICCD.QuestBot.Quests.Triggers;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;

public class TriggerContentCreatorSkillContractorConfirm : QuestReactionTrigger<Step3SkillsContractor>
{
    private Type? _nextStepType;
    private QuestTriggerResult _triggerResult;
    public override string Name => "Confirm";

    public override Type NextStepType => _nextStepType;

    public override QuestTriggerResult TriggerResult => _triggerResult;

    public override uint OrderSequence { get; } = 100;

    public override Task Init(Step3SkillsContractor parentStep, QuestContext context)
    {
        _nextStepType = typeof(Step3SecondaryPurpose);
        if (parentStep.ParentQuest.QuestSteps.Count(x =>
                x.Status == IQuestStep.StepStatus.Completed && x.GetType().BaseType == typeof(Step3PurposeBase)) == 2)
        {
            // // 2nd run, jump to  Step4
            // _nextStepType = typeof(Step4Acknowledge);
            // 2nd run, we are done.
            _triggerResult = QuestTriggerResult.Success;
        }

        return base.Init(parentStep, context);
    }

    public override IEmote ReactionEmote { get; } = Emoji.Parse(":white_check_mark:");
}