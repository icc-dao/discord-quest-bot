using System;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.Triggers;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step2.Triggers;

public class Page2Next  : QuestReactionTrigger<Step2DaoPrinciples2>
{
    public override string Name { get; } = "Next";
    public override Type NextStepType => typeof(Step2DaoPrinciples3);
    public override QuestTriggerResult TriggerResult => QuestTriggerResult.Continue;
    public override uint OrderSequence => 1;
    
    public override IEmote ReactionEmote => Emoji.Parse(":arrow_right:");
}