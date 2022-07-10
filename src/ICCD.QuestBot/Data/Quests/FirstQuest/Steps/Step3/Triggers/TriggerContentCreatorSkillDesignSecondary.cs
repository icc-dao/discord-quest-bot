using System;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.TriggerAction;
using ICCD.QuestBot.Quests.Triggers;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;

public class TriggerContentCreatorSkillDesignSecondary : QuestReactionTrigger<Step3SkillsContentCreatorSecondary>
{
    public TriggerContentCreatorSkillDesignSecondary()
    {
        AddTriggerAction(new AddRoleTriggerAction("Designers / Artists"));
    }

    public override string Name => "Design/Art";
    public override Type NextStepType { get; }
    public override QuestTriggerResult TriggerResult { get; }
    public override uint OrderSequence { get; }


    public override IEmote ReactionEmote { get; } = Emoji.Parse(":frame_photo:");
}