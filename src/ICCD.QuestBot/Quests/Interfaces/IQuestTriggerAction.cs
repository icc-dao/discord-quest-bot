using System.Threading.Tasks;

namespace ICCD.QuestBot.Quests.Interfaces;

/// <summary>
/// A quest trigger action.
/// </summary>
public interface IQuestTriggerAction
{
    /// <summary>
    /// The up action.
    /// </summary>
    /// <param name="questContext">The quest context.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task ExecuteUp(QuestContext questContext);

    /// <summary>
    /// The down action.
    /// </summary>
    /// <param name="questContext">The quest context.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task ExecuteDown(QuestContext questContext);
}