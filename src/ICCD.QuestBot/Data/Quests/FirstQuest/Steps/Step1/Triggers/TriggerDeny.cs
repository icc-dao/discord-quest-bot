using System;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.Triggers;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;

public class TriggerDeny : QuestReactionTrigger<Step1SafetyGuidelines>
{
    public override string Name { get; } = "Deny Safety Guidelines";

    public override Type NextStepType => null;

    public override QuestTriggerResult TriggerResult { get; } = QuestTriggerResult.Failure;

    public override uint OrderSequence => 1;
    
    public override IEmote ReactionEmote { get; } = Emoji.Parse(":x:");
}