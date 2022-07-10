using System;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.TriggerAction;
using ICCD.QuestBot.Quests.Triggers;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;

public class TriggerContentCreatorSkillCreativeContractor : QuestReactionTrigger<Step3SkillsContractor>
{
    public TriggerContentCreatorSkillCreativeContractor()
    {
        AddTriggerAction(new AddRoleTriggerAction("LF: Creatives"));
    }

    public override string Name => "Creatives";
    public override Type NextStepType { get; }
    public override QuestTriggerResult TriggerResult { get; }
    public override uint OrderSequence { get; }
    public override IEmote ReactionEmote { get; } = Emoji.Parse(":mage:");
}