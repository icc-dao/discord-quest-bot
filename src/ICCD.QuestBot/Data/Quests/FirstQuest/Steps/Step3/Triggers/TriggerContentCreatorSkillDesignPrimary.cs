using System;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.TriggerAction;
using ICCD.QuestBot.Quests.Triggers;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;

public class TriggerContentCreatorSkillDesignPrimary : QuestReactionTrigger<Step3SkillsContentCreatorPrimary>
{

    public TriggerContentCreatorSkillDesignPrimary()
    {
        AddTriggerAction(new AddRoleTriggerAction("Designer / Artist"));
        AddTriggerAction(new AddRoleTriggerAction("Designers / Artists"));
    }

    public override string Name => "Design/Art";
    public override Type NextStepType => typeof(Step3SkillsContentCreatorSecondary);
    public override QuestTriggerResult TriggerResult { get; }
    public override uint OrderSequence { get; }


    public override IEmote ReactionEmote { get; } = Emoji.Parse(":frame_photo:");
}