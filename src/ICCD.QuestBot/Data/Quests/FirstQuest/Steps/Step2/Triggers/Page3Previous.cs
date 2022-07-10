using System;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.Triggers;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step2.Triggers;

public class Page3Previous  : QuestReactionTrigger<Step2DaoPrinciples3>
{
    public override string Name { get; } = "Previous";
    public override Type NextStepType => typeof(Step2DaoPrinciples2);
    public override QuestTriggerResult TriggerResult => QuestTriggerResult.Continue;
    public override uint OrderSequence => 0;
    public override IEmote ReactionEmote => Emoji.Parse(":arrow_left:");
}