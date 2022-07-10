using System;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.TriggerAction;
using ICCD.QuestBot.Quests.Triggers;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;

public class TriggerContentCreatorSkillDevelopingContractor: QuestReactionTrigger<Step3SkillsContractor>
{
    public TriggerContentCreatorSkillDevelopingContractor()
    {
        // CMD + .
        AddTriggerAction(new AddRoleTriggerAction("LF: Developers"));
    }

    public override string Name => "Developers";
    public override Type NextStepType { get; }
    public override QuestTriggerResult TriggerResult { get; }
    public override uint OrderSequence { get; }
    public override IEmote ReactionEmote { get; } = Emoji.Parse(":tools:");
}