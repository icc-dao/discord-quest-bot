using Discord.WebSocket;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Quests;

public class QuestContext
{
    public QuestContext(SocketGuildUser socketGuildUser)
    {
        SocketGuildUser = socketGuildUser;
    }
    
    public SocketGuildUser? SocketGuildUser { get; }

    public IQuestStep? CurrentExecutionStep { get; set; }
}