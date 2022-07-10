using System;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.Triggers;
using ICCD.QuestBot.Quests.TriggerAction;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;

public class TriggerContentCreatorSkillWebDesignContractor: QuestReactionTrigger<Step3SkillsContractor>
{
    public TriggerContentCreatorSkillWebDesignContractor()
    {
        AddTriggerAction(new AddRoleTriggerAction("LF: Web/UI/UX Designers"));
    }
    public override string Name => "Web/UI/UX Designers";
    public override Type NextStepType { get; }
    public override QuestTriggerResult TriggerResult { get; }
    public override uint OrderSequence { get; }

    public override IEmote ReactionEmote { get; } = Emoji.Parse(":computer:");
}