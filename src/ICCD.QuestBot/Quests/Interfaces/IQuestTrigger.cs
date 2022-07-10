using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using ICCD.QuestBot.Quests.Enums;

namespace ICCD.QuestBot.Quests.Interfaces;

/// <summary>
/// A quest trigger.
/// </summary>
public interface IQuestTrigger
{
    /// <summary>
    /// The quest trigger name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets or sets a value indicating whether to skip the quest trigger.
    /// </summary>
    bool Skip { get; set; }

    /// <summary>
    /// Gets or sets the quest trigger status.
    /// </summary>
    TriggerStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the quest context.
    /// </summary>
    QuestContext Context { get; set; }

    /// <summary>
    /// Gets the type of the next step.
    /// </summary>
    Type? NextStepType { get; }

    /// <summary>
    /// Gets the type of the quest action.
    /// </summary>
    QuestActionType Type { get; }

    /// <summary>
    /// Gets the quest trigger result.
    /// </summary>
    QuestTriggerResult TriggerResult { get; }

    /// <summary>
    /// Gets the trigger order sequence.
    /// </summary>
    uint OrderSequence { get; }

    /// <summary>
    /// Executed when the action is triggered..
    /// </summary>
    void Trigger();

    /// <summary>
    /// Executed when the action trigger is removed.
    /// </summary>
    void Untrigger();

    /// <summary>
    /// Runs the trigger.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task Run();

    /// <summary>
    /// Executes the trigger action.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task Action();

    /// <summary>
    /// Transforms the step messages.
    /// </summary>
    /// <param name="eb">The step message's embed builder.</param>
    /// <returns>An embed builder containing the transformed messaged.</returns>
    EmbedBuilder TransformStepMessage(EmbedBuilder eb);

    /// <summary>
    /// A collection of quest trigger actions.
    /// </summary>
    ICollection<IQuestTriggerAction> QuestTriggerActions { get; }

    /// <summary>
    /// A quest trigger status.
    /// </summary>
    public enum TriggerStatus
    {
        /// <summary>
        /// The quest trigger is new.
        /// </summary>
        New,

        /// <summary>
        /// the quest trigger is initialised and ready to be ran.
        /// </summary>
        Initialised,

        /// <summary>
        /// The quest trigger is running.
        /// </summary>
        Running,

        /// <summary>
        /// The quest trigger is triggered.
        /// </summary>
        Triggered,

        /// <summary>
        /// The quest trigger is completed.
        /// </summary>
        Completed
    }
}

/// <summary>
/// A quest trigger.
/// </summary>
/// <typeparam name="TParentStep">The parent step type.</typeparam>
public interface IQuestTrigger<TParentStep> : IQuestTrigger where TParentStep : IQuestStep
{
    /// <summary>
    /// The parent step.
    /// </summary>
    TParentStep ParentStep { get; set; }

    /// <summary>
    /// Initialises the quest trigger.
    /// </summary>
    /// <param name="parentStep">The parent step.</param>
    /// <param name="context">The quest context.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task Init(TParentStep parentStep, QuestContext context);
}