using System;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.TriggerAction;
using ICCD.QuestBot.Quests.Triggers;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;

public class TriggerContentCreatorSkillDesignContractor: QuestReactionTrigger<Step3SkillsContractor>
{
    public TriggerContentCreatorSkillDesignContractor()
    {
        AddTriggerAction(new AddRoleTriggerAction("LF: Designers / Artists"));
    }
    public override string Name => "Designer/Artists";
    public override Type NextStepType { get; }
    public override QuestTriggerResult TriggerResult { get; }
    public override uint OrderSequence { get; }

    public override IEmote ReactionEmote { get; } = Emoji.Parse(":frame_photo:");
}