using System;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.Triggers;
using ICCD.QuestBot.Quests.TriggerAction;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;

public class TriggerContentCreatorSkillEducatingPrimary : QuestReactionTrigger<Step3SkillsContentCreatorPrimary>
{
    public TriggerContentCreatorSkillEducatingPrimary()
    {
        AddTriggerAction(new AddRoleTriggerAction("Educator"));
        AddTriggerAction(new AddRoleTriggerAction("Educators"));
    }
    public override string Name => "Educating";
    public override Type NextStepType => typeof(Step3SkillsContentCreatorSecondary);
    public override QuestTriggerResult TriggerResult { get; }
    public override uint OrderSequence { get; }
   

    public override IEmote ReactionEmote { get; } = Emoji.Parse(":man_student:");
}