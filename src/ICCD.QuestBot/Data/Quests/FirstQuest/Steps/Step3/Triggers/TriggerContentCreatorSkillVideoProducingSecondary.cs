using System;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.Triggers;
using ICCD.QuestBot.Quests.TriggerAction;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;

public class TriggerContentCreatorSkillVideoProducingSecondary: QuestReactionTrigger<Step3SkillsContentCreatorSecondary>
{
    public TriggerContentCreatorSkillVideoProducingSecondary()
    {
        AddTriggerAction(new AddRoleTriggerAction("Video Producers"));
    }
    public override string Name => "Video Producing";
    public override Type NextStepType { get; }
    public override QuestTriggerResult TriggerResult { get; }
    public override uint OrderSequence { get; }

    public override IEmote ReactionEmote { get; } = Emoji.Parse(":video_camera:");
}