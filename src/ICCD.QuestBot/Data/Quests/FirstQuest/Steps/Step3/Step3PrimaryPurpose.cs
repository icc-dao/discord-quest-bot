using ICCD.QuestBot.Quests;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1;

public class Step3PrimaryPurpose : Step3PurposeBase
{
    public Step3PrimaryPurpose(IQuest parentQuest, QuestContext context) : base(parentQuest, context)
    {
    }

    public override string Title => "Identify your primary purpose for joining the Discord";
    public override string Description => "Choose one of the Emojis below.";
}