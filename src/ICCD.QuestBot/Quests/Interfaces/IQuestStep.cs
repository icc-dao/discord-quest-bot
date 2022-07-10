using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;

namespace ICCD.QuestBot.Quests.Interfaces;

/// <summary>
/// A quest step.
/// </summary>
public interface IQuestStep
{
    /// <summary>
    /// Gets the quest step title.
    /// </summary>
    string Title { get; }

    /// <summary>
    /// Gets the quest step description.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Gets or sets the quest step status.
    /// </summary>
    StepStatus Status { get; set; }

    /// <summary>
    /// Gets the current message.
    /// </summary>
    IMessage CurrentMessage { get; }

    /// <summary>
    /// Gets the parent quest.
    /// </summary>
    IQuest ParentQuest { get; }

    /// <summary>
    /// Gets the quest context.
    /// </summary>
    QuestContext Context { get; }

    /// <summary>
    /// Gets a collection of quest triggers.
    /// </summary>
    ICollection<IQuestTrigger> Triggers { get; }

    /// <summary>
    /// Executes the quest step.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task Execute();

    /// <summary>
    /// The step status.
    /// </summary>
    public enum StepStatus
    {
        /// <summary>
        /// The step is new.
        /// </summary>
        New,
        /// <summary>
        /// The step is initialised.
        /// </summary>
        Initialised,
        /// <summary>
        /// The step is running.
        /// </summary>
        Running,
        /// <summary>
        /// The step is completed.
        /// </summary>
        Completed
    }
}