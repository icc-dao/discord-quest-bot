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

public class TriggerPurposeContractor : QuestReactionTrigger<Step3PurposeBase>
{
    public TriggerPurposeContractor()
    {
        AddTriggerAction(new AddRoleTriggerAction("Contractor"));
    }
    
    public override string Name => "Contractor";
    public override Type NextStepType { get; } = typeof(Step3SkillsContractor);
    public override QuestTriggerResult TriggerResult { get; }
    public override uint OrderSequence => 20;
    
    public override Task Init(Step3PurposeBase parentStep, QuestContext context)
    {
        if (parentStep.ParentQuest.QuestSteps.SelectMany(x => x.Triggers.Where(x => x.Status == IQuestTrigger.TriggerStatus.Triggered)).Any(x => x.GetType() == this.GetType()))
        {
            Skip = true;
            return Task.CompletedTask;
        }
        return base.Init(parentStep, context);
    }

    public override IEmote ReactionEmote { get; } = Emoji.Parse(":moneybag:");
}