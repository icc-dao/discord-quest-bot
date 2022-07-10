using System;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.Triggers;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step2.Triggers;

public class TriggerDisagree : QuestReactionTrigger<Step2DaoPrinciplesBase>
{
    public override string Name { get; } = "Disagree to DAO Principles";
    public override Type NextStepType { get; }
    public override QuestActionType Type { get; }
    public override IEmote ReactionEmote { get; } = Emoji.Parse(":x:");
    public override QuestTriggerResult TriggerResult { get; } = QuestTriggerResult.Failure;
    public override uint OrderSequence { get; } = 100;
}