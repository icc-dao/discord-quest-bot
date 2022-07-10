using System;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.TriggerAction;
using ICCD.QuestBot.Quests.Triggers;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;

public class TriggerContentCreatorSkillDevelopingSecondary : QuestReactionTrigger<Step3SkillsContentCreatorSecondary>
{
    public TriggerContentCreatorSkillDevelopingSecondary()
    {
        // CMD + K + D
        // CMD + . for recommendations
        AddTriggerAction(new AddRoleTriggerAction("Developers"));
    }

    public override string Name => "Developing";
    public override Type NextStepType { get; }
    public override QuestTriggerResult TriggerResult { get; }
    public override uint OrderSequence { get; }


    public override IEmote ReactionEmote { get; } = Emoji.Parse(":tools:");
}