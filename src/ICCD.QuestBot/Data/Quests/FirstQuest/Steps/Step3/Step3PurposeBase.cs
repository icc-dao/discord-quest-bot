using ICCD.QuestBot.Quests;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1;

public abstract class Step3PurposeBase : BaseQuestStep
{
    protected Step3PurposeBase(IQuest parentQuest, QuestContext context) : base(parentQuest, context)
    {
    }
}