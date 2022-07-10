using System;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.Triggers;
using ICCD.QuestBot.Quests.TriggerAction;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;

public class TriggerContentCreatorSkillWritingContractor: QuestReactionTrigger<Step3SkillsContractor>
{
    public TriggerContentCreatorSkillWritingContractor()
    {
        AddTriggerAction(new AddRoleTriggerAction("LF: Writers"));
    }
    public override string Name => "Writers";
    public override Type NextStepType { get; }
    public override QuestTriggerResult TriggerResult { get; }
    public override uint OrderSequence { get; }


    public override IEmote ReactionEmote { get; } = Emoji.Parse(":pencil:");
}