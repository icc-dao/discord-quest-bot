// <copyright file="IQuest.cs" company="ICCD">
// Copyright (c) ICCD. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;

namespace ICCD.QuestBot.Quests.Interfaces;

/// <summary>
/// A quest.
/// </summary>
public interface IQuest
{
    /// <summary>
    /// Gets the quest ID.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the quest context.
    /// </summary>
    QuestContext Context { get; }

    /// <summary>
    /// Gets the quest title.
    /// </summary>
    string Title { get; }

    /// <summary>
    /// Gets the quest status.
    /// </summary>
    Status QuestStatus { get; }

    /// <summary>
    /// Gets the executing quest steps.
    /// </summary>
    List<Task> ExecutingQuestSteps { get; }

    /// <summary>
    /// Gets a collection of this quest's steps.
    /// </summary>
    ICollection<IQuestStep> QuestSteps { get; }

    /// <summary>
    /// Method called on reset.
    /// Resets the quest.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task ResetAction();

    /// <summary>
    /// Sends a message to a user or channel.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="embed">The embed to send.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<IMessage> SendMessage(string? message = null, Embed? embed = null);

    /// <summary>
    /// Sends a status message to the status message channel.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="embed">The embed to send.</param>
    /// <param name="cleanupTimer">The amount of time the message should be displayed for in ms: 0 = infinite.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task SendStatus(string? message, Embed? embed, ulong cleanupTimer);

    /// <summary>
    /// Replaces variables in a text.
    /// </summary>
    /// <param name="text">The input text.</param>
    /// <returns>The replaced text.</returns>
    string ReplaceVariables(string text);

    /// <summary>
    /// Executes the quest.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task Execute();
}