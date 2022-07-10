using ICCD.QuestBot.Quests;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step2;

public abstract class Step2DaoPrinciplesBase : BaseQuestStep
{
    protected Step2DaoPrinciplesBase(IQuest parentQuest, QuestContext context) : base(parentQuest, context)
    {
    }
}