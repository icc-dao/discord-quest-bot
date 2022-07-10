using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using ICCD.QuestBot.Data.Quests;
using ICCD.QuestBot.Quests;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.Interfaces;
using ICCD.QuestBot.Quests.Triggers;

namespace ICCD.QuestBot.Services;

public class QuestManager
{
    private readonly QuestConfigurationManager _questConfigurationManager;
    private readonly List<IQuest> _allQuests = new();

    public QuestManager(QuestConfigurationManager questConfigurationManager)
    {
        _questConfigurationManager = questConfigurationManager;
    }

    public void ProcessOnJoinQuests(SocketGuildUser socketGuildUser)
    {
        var allOnJoinQuests = _questConfigurationManager.GetAll(QuestStartTrigger.OnJoin);
        foreach (var onJoinQuest in allOnJoinQuests)
        {
            if (!UserHasQuestCompleted(socketGuildUser, onJoinQuest.QuestId) &&
                !UserHasQuestActive(socketGuildUser, onJoinQuest.QuestId))
            {
                StartQuest(socketGuildUser, onJoinQuest.QuestId);
            }
        }
    }

    public async Task ProcessReaction(IUser user, ulong messageId, IEmote emote, ReactionAction reactionAction)
    {
        Console.WriteLine($"{messageId}: {reactionAction.ToString()} - {user.Username} Reaction¨: {emote.Name}");
        foreach (var quest in _allQuests.Where(x => x.QuestStatus.Code.Equals(StatusCode.Running)))
        {
            var step = quest.QuestSteps.FirstOrDefault(x => x.CurrentMessage.Id.Equals(messageId));
            if (step == null)
            {
                continue;
            }

            var questReactionTrigger = step.Triggers.OfType<IQuestReactionTrigger>().FirstOrDefault(x => x.ReactionEmote.Equals(emote));
            if (questReactionTrigger != null)
            {
                if (reactionAction == ReactionAction.Add)
                {
                    questReactionTrigger.Trigger();
                }
                else
                {
                    questReactionTrigger.Untrigger();
                }
            }
        }
    }

    public enum ReactionAction
    {
        Remove,
        Add
    }

    public async Task StartQuest(SocketGuildUser socketGuildUser, string questId, bool force = false)
    {
        var questMapEntry = _questConfigurationManager.Get(questId);
        var questContext = new QuestContext(socketGuildUser);

        var quest = Activator.CreateInstance(questMapEntry.QuestType, new object[] { questContext }) as IQuest;
        if (quest == null)
        {
            throw new ArgumentException(
                $"Couldn't create quest instance for type {questMapEntry.QuestType.Name}. Specified by questId.",
                nameof(questId));
        }

        if (UserHasQuestActive(socketGuildUser, questMapEntry.QuestId))
        {
            return;
        }

        if (UserHasQuestCompleted(socketGuildUser, questMapEntry.QuestId))
        {
            if (force)
            {
                await quest.ResetAction();
            }
            else
            {
                return;
            }
        }

        _allQuests.Add(quest);
        quest.Execute();
    }

    public bool UserHasQuestCompleted(SocketGuildUser socketGuildUser, string questId)
    {
        var mapEntry = _questConfigurationManager.Get(questId);
        if (socketGuildUser.Roles.Any(x => x.Id.Equals(mapEntry.QuestCompletionRoleId)))
        {
            return true;
        }

        return false;
    }

    public bool UserHasQuestActive(IUser user, string questId)
    {
        var mapEntry = _questConfigurationManager.Get(questId);
        lock (_allQuests)
        {
            return _allQuests.Any(x =>
                (x.QuestStatus.Code == StatusCode.Initialised || x.QuestStatus.Code == StatusCode.Running) &&
                x.Context.SocketGuildUser!.Id.Equals(user.Id) && x.GetType() == mapEntry.QuestType);
        }
    }
}