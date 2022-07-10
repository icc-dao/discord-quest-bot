using System;
using Discord;
using ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.Triggers;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step2.Triggers;

public class Page5Agree : QuestReactionTrigger<Step2DaoPrinciples5>
{
    public override string Name => "Agree to DAO Principles";

    public override Type NextStepType => typeof(Step3PrimaryPurpose);
    public override QuestActionType Type { get; }

    public override QuestTriggerResult TriggerResult { get; } = QuestTriggerResult.Continue;

    public override uint OrderSequence => 1;

    public override IEmote ReactionEmote { get; } = Emoji.Parse(":white_check_mark:");
}