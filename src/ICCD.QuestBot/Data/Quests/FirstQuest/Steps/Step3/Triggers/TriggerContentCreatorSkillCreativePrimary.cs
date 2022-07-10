using System;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.TriggerAction;
using ICCD.QuestBot.Quests.Triggers;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;

public class TriggerContentCreatorSkillCreativePrimary : QuestReactionTrigger<Step3SkillsContentCreatorPrimary>
{
    public TriggerContentCreatorSkillCreativePrimary()
    {
        AddTriggerAction(new AddRoleTriggerAction("Creative"));
        AddTriggerAction(new AddRoleTriggerAction("Creatives"));
    }
    
    public override string Name => "Creative";
    public override Type NextStepType => typeof(Step3SkillsContentCreatorSecondary);
    public override QuestTriggerResult TriggerResult { get; }
    public override uint OrderSequence { get; }

    public override IEmote ReactionEmote { get; } = Emoji.Parse(":mage:");
}