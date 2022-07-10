// <copyright file="TriggerAgree.cs" company="ICCD">
// Copyright (c) ICCD. All rights reserved.
// </copyright>

using System;
using Discord;
using ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step2;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.Triggers;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;

public class TriggerAgree : QuestReactionTrigger<Step1SafetyGuidelines>
{
    public override string Name => "Agree to Safety Guidelines";

    public override Type NextStepType => typeof(Step2DaoPrinciples1);

    public override QuestActionType Type { get; }

    public override QuestTriggerResult TriggerResult => QuestTriggerResult.Continue;

    public override uint OrderSequence => 0;

    public override IEmote ReactionEmote { get; } = Emoji.Parse(":white_check_mark:");
}