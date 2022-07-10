// <copyright file="Step1SafetyGuidelines.cs" company="ICCD">
// Copyright (c) ICCD. All rights reserved.
// </copyright>

using System.Text;
using ICCD.QuestBot.Quests;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1;

/// <summary>
/// Safety Guidelines Step.
/// </summary>
public class Step1SafetyGuidelines : BaseQuestStep
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Step1SafetyGuidelines"/> class.
    /// </summary>
    /// <param name="parentQuest">The parent quest.</param>
    /// <param name="context">The quest context.</param>
    public Step1SafetyGuidelines(IQuest parentQuest, QuestContext context)
        : base(parentQuest, context)
    {
    }

    /// <inheritdoc />
    public override string Title => "Acknowledge the following.";

    /// <inheritdoc />
    public override string Description
    {
        get
        {
            var sb = new StringBuilder();
            sb.AppendLine(
                "1. **WE WILL NEVER SEND YOU A DIRECT MESSAGE FIRST.** Treat any message offering DAO assistance, investment opportunities, or other offers as suspicious, and use our #report-spammer-scammer channel to report the instance.");
            sb.AppendLine(
                "2. Never reveal private keys for your wallet or click unsolicited links that you receive in your DMs.");
            return sb.ToString();
        }
    }
}