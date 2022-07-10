using System.Text;
using Discord;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Quests;

public class Status
{
    public Status(StatusCode code = StatusCode.Initialised, string statusMessage = null, string statusReason = null)
    {
        Code = code;
        StatusReason = statusReason ?? string.Empty;
        StatusMessage = statusMessage ?? string.Empty;
    }

    public StatusCode Code { get; private set; }

    public string StatusMessage { get; private set; }

    public string StatusReason { get; private set; }

    public void SetStatus(StatusCode code, string message = null, string reason = null)
    {
        Code = code;
        StatusMessage = message ?? string.Empty;
        StatusReason = reason ?? string.Empty;
    }

    public void Success(string message = null, string reason = null)
    {
        SetStatus(StatusCode.Succeeded, message, reason);
    }

    public void Fail(string message = null, string reason = null)
    {
        SetStatus(StatusCode.Failed, message, reason);
    }

    public Embed ToEmbed(IQuest quest)
    {
        var eb = new EmbedBuilder().WithCurrentTimestamp();
        var sb = new StringBuilder();
        
        switch (Code)
        {
            case StatusCode.Running:
            case StatusCode.Initialised:
                eb = eb.WithTitle($"Quest {quest.Title} ended prematurely.").WithColor(Color.Red);
                break;
            case StatusCode.Failed:
                eb = eb.WithTitle($"Quest {quest.Title} failed.").WithColor(Color.Red);
                break;
            case StatusCode.Succeeded:
                eb = eb.WithTitle($"Quest {quest.Title} completed!").WithColor(Color.Green);
                break;
        }
        
        if (string.IsNullOrEmpty(StatusMessage))
        {
            switch (Code)
            {
                case StatusCode.Running:
                case StatusCode.Initialised:
                    sb.AppendLine($"The quest stopped unexpectedly.");
                    break;
                case StatusCode.Failed:
                    sb.AppendLine($"The quest failed.");
                    break;
                case StatusCode.Succeeded:
                    sb.AppendLine($"Thank you for completing the Quest.");
                    break;
            }
        }
        else
        {
            sb.Append(StatusMessage);
        }

        if (!string.IsNullOrEmpty(StatusReason))
        {
            sb.AppendLine("Reason: " + StatusReason);
        }

        if (Code is StatusCode.Failed or StatusCode.Initialised or StatusCode.Running)
        {
            sb.AppendLine($"**Please type ``!quest restart {quest.Id}`` to restart the quest.**");
        }

        eb = eb.WithDescription(sb.ToString());
        return eb.Build();
    }
}

public enum StatusCode
{
    Initialised,
    Running,
    Failed,
    Succeeded
}