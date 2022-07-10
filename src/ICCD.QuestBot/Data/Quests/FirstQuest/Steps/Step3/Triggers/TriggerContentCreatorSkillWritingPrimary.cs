using System;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.Triggers;
using ICCD.QuestBot.Quests.TriggerAction;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;

public class TriggerContentCreatorSkillWritingPrimary : QuestReactionTrigger<Step3SkillsContentCreatorPrimary>
{
    public TriggerContentCreatorSkillWritingPrimary()
    {
        AddTriggerAction(new AddRoleTriggerAction("Writer"));
        AddTriggerAction(new AddRoleTriggerAction("Writers"));
    }
    public override string Name => "Writing";
    public override Type NextStepType => typeof(Step3SkillsContentCreatorSecondary);
    public override QuestTriggerResult TriggerResult { get; }
    public override uint OrderSequence { get; }


    public override IEmote ReactionEmote { get; } = Emoji.Parse(":pencil:");
}