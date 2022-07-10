namespace ICCD.QuestBot.Quests.Enums;

/// <summary>
/// A quest start trigger.
/// </summary>
public enum QuestStartTrigger
{
    /// <summary>
    /// Quest doesn't auto start.
    /// </summary>
    None,

    /// <summary>
    /// Quest starts when user joins.
    /// </summary>
    OnJoin,
}