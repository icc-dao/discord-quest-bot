// <copyright file="QuestMap.cs" company="ICCD">
// Copyright (c) ICCD. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using ICCD.QuestBot.Data.Quests.FirstQuest;
using ICCD.QuestBot.Quests;
using ICCD.QuestBot.Quests.Enums;

namespace ICCD.QuestBot.Data.Quests;

/// <summary>
/// Manages the quest configurations.
/// </summary>
public class QuestConfigurationManager
{
    private readonly ICollection<QuestConfiguration> _questConfigurations = new List<QuestConfiguration>();

    private QuestConfigurationManager()
    {
        AddMapEntry<WelcomeQuest>("first-quest", QuestStartTrigger.OnJoin, 918505873050058832, new QuestConfiguration.QuestMessageContext(953692240658710528));
    }

    /// <summary>
    /// Singleton implementation.
    /// </summary>
    public static QuestConfigurationManager Instance => new();

    private void AddMapEntry<TQuestType>(string questId, QuestStartTrigger startTrigger, ulong questComleteRoleId, QuestConfiguration.QuestMessageContext? messageContext = null) where TQuestType : BaseQuest
    {
        var questConfiguration =
            new QuestConfiguration(questId, typeof(TQuestType), startTrigger, questComleteRoleId, messageContext);
        if (_questConfigurations.Any(x =>
                x.QuestId.Equals(questConfiguration.QuestId, StringComparison.OrdinalIgnoreCase) ||
                x.QuestType == questConfiguration.QuestType))
        {
            throw new ArgumentException(
                $"Quest with ID {questConfiguration.QuestId} or type {questConfiguration.QuestType.FullName} was already added.",
                nameof(questConfiguration));
        }

        lock (questConfiguration)
        {
            _questConfigurations.Add(questConfiguration);
        }
    }

    /// <summary>
    /// Gets all quest configuration.
    /// </summary>
    /// <param name="startTrigger">The start trigger type of which</param>
    /// <returns>A collection of quest configurations.</returns>
    public ICollection<QuestConfiguration> GetAll(QuestStartTrigger? startTrigger = null)
    {
        return startTrigger == null
            ? _questConfigurations.ToList()
            : _questConfigurations.Where(x => x.StartTrigger.Equals(startTrigger)).ToList();
    }

    /// <summary>
    /// Gets a quest configuration by quest ID.
    /// </summary>
    /// <param name="questId">The quest ID.</param>
    /// <returns>The quest configuration.</returns>
    public QuestConfiguration Get(string questId)
    {
        return Get(questId, null);
    }

    /// <summary>
    /// Gets a quest configuration by quest type.
    /// </summary>
    /// <param name="questType">The quest type.</param>
    /// <returns>The quest configuration.</returns>
    public QuestConfiguration Get(Type questType)
    {
        return Get(null, questType);
    }

    private QuestConfiguration Get(string? questId, Type? questType)
    {
        QuestConfiguration? questConfiguration = null;
        if (questId != null)
        {
            questConfiguration = _questConfigurations
                .SingleOrDefault(x => x.QuestId.Equals(questId, StringComparison.OrdinalIgnoreCase));
        }

        if (questType != null)
        {
            questConfiguration = _questConfigurations.SingleOrDefault(x => x.QuestType == questType);
        }

        return questConfiguration ?? throw new ArgumentException($"Quest was not found.");
    }

    public class QuestConfiguration
    {
        public string QuestId { get; }

        public QuestStartTrigger StartTrigger { get; }

        public ulong QuestCompletionRoleId { get; }

        public Type QuestType { get; }

        public QuestMessageContext MessageContext { get; }

        public QuestConfiguration(string questId, Type questType, QuestStartTrigger startTrigger, ulong questCompletionRoleId,
            QuestMessageContext? messageContext = null)
        {
            QuestId = questId;
            StartTrigger = startTrigger;
            QuestCompletionRoleId = questCompletionRoleId;
            QuestType = questType;
            MessageContext = messageContext ?? new QuestMessageContext();
        }

        public class QuestMessageContext
        {
            public QuestMessageContext()
            {
                MessageChannel = QuestMessageChannel.DM;
            }

            public QuestMessageContext(ulong statusMessageChannelId)
            {
                MessageChannel = QuestMessageChannel.DM;
                StatusMessageChannelId = statusMessageChannelId;
            }

            public QuestMessageContext(ulong messageChannelId, ulong statusMessageChannelId)
            {
                MessageChannel = QuestMessageChannel.Channel;
                MessageChannelId = messageChannelId;
                StatusMessageChannelId = statusMessageChannelId;
            }

            public QuestMessageChannel MessageChannel { get; }

            public ulong? MessageChannelId { get; }

            public ulong? StatusMessageChannelId { get; }

            public enum QuestMessageChannel
            {
                DM,
                Channel,
            }
        }
    }
}