using System.Threading;
using System.Threading.Tasks;
using Discord;
using ICCD.QuestBot.Quests.Enums;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Quests.Triggers;

/// <summary>
/// A quest reaction trigger.
/// </summary>
public interface IQuestReactionTrigger : IQuestTrigger
{
    /// <summary>
    /// Gets the reaction emote.
    /// </summary>
    IEmote ReactionEmote { get; }
}

/// <summary>
/// Abstract implementation of the <see cref="IQuestReactionTrigger"/> interface.
/// </summary>
/// <typeparam name="TQuestStep">Step type for which this trigger should apply.</typeparam>
public abstract class QuestReactionTrigger<TQuestStep> : BaseQuestTrigger<TQuestStep>, IQuestReactionTrigger
    where TQuestStep : BaseQuestStep, IQuestStep
{
    /// <inheritdoc />
    public override QuestActionType Type { get; } =
        QuestActionType.ReactionAction;

    /// <inheritdoc />
    public abstract IEmote ReactionEmote { get; }

    /// <inheritdoc />
    public override async Task Init(TQuestStep parentStep, QuestContext context)
    {
        await base.Init(parentStep, context);
        if (ParentStep.StepCompleted)
        {
            Status = IQuestTrigger.TriggerStatus.Completed;
            return;
        }

        await ParentStep.CurrentMessage.AddReactionAsync(ReactionEmote);
        Status = IQuestTrigger.TriggerStatus.Initialised;
    }

    /// <inheritdoc />
    public override EmbedBuilder TransformStepMessage(EmbedBuilder eb)
    {
        if (Skip)
        {
            return eb;
        }

        if (!string.IsNullOrEmpty(Name))
        {
            eb.AddField(Name, ReactionEmote.ToString(), true);
        }

        return eb;
    }
}