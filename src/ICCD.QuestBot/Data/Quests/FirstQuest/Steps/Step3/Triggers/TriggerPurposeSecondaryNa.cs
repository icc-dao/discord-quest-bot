using System;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.Triggers;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;

public class TriggerPurposeSecondaryNa: QuestReactionTrigger<Step3SecondaryPurpose>
{
    public override string Name => "N/A";
    public override Type NextStepType { get; }
    public override QuestTriggerResult TriggerResult { get; } = QuestTriggerResult.Success;
    public override uint OrderSequence { get; } = 100;
    public override IEmote ReactionEmote { get; } = Emoji.Parse(":x:");
}