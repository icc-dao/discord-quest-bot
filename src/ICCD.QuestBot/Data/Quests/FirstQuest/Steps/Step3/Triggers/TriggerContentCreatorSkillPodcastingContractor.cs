using System;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.Triggers;
using ICCD.QuestBot.Quests.TriggerAction;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;

public class TriggerContentCreatorSkillPodcastingContractor: QuestReactionTrigger<Step3SkillsContractor>
{
    public TriggerContentCreatorSkillPodcastingContractor()
    {
        AddTriggerAction(new AddRoleTriggerAction("LF: Podcasters"));
    }
    public override string Name => "Podcasters";
    public override Type NextStepType { get; }
    public override QuestTriggerResult TriggerResult { get; }
    public override uint OrderSequence { get; }

    public override IEmote ReactionEmote { get; } = Emoji.Parse(":microphone2:");
}