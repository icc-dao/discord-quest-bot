using System;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.Triggers;
using ICCD.QuestBot.Quests.TriggerAction;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;

public class TriggerContentCreatorSkillWebDesignPrimary : QuestReactionTrigger<Step3SkillsContentCreatorPrimary>
{
    public TriggerContentCreatorSkillWebDesignPrimary()
    {
        AddTriggerAction(new AddRoleTriggerAction("Web/UI/UX Designer"));
        AddTriggerAction(new AddRoleTriggerAction("Web/UI/UX Designers"));
    }
    public override string Name => "Web/UI/UX Design";
    public override Type NextStepType => typeof(Step3SkillsContentCreatorSecondary);
    public override QuestTriggerResult TriggerResult { get; }
    public override uint OrderSequence { get; }


    public override IEmote ReactionEmote { get; } = Emoji.Parse(":computer:");
}