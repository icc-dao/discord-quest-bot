using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using ICCD.QuestBot.Data.Quests.FirstQuest;
using ICCD.QuestBot.Quests;
using ICCD.QuestBot.Services;

namespace ICCD.QuestBot.Modules;

[Group("quest")]
public class QuestModule : ModuleBase<SocketCommandContext>
{
    private readonly QuestManager _questManager;

    public QuestModule(QuestManager questManager)
    {
        _questManager = questManager;
    }

    [RequireContext(ContextType.Guild, ErrorMessage = "Sorry, this command must be ran from within a server, not via DM!")]
    [Command("start")]
    public async Task StartQuest(string questId)
    {
        await _questManager.StartQuest(Context.User as SocketGuildUser, questId);
    }

    [RequireContext(ContextType.Guild, ErrorMessage = "Sorry, this command must be ran from within a server, not via DM!")]
    [Command("restart")]
    public async Task RestartQuest(string questId)
    {
        await _questManager.StartQuest(Context.User as SocketGuildUser, questId, true);
    }

    [Command("test")]
    public async Task TestQuest()
    {
        var context = new QuestContext(Context.Guild.GetUser(Context.User.Id));
        WelcomeQuest fq = new WelcomeQuest(context);
        fq.Execute();
    }

    [Command("exportroles")]
    public async Task ExportRoles()
    {
        var roles = Context.Guild.Roles.Select(x => new { Id = x.Id, Name = x.Name });
        var sb = new StringBuilder("```");
        sb.AppendLine("id;name");
        foreach (var role in roles.OrderBy(x => x.Name))
        {
            sb.AppendLine($"{role.Id};{role.Name}");
        }

        sb.AppendLine("```");
        await Context.Channel.SendMessageAsync(sb.ToString());
    }
}