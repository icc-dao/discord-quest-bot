using ICCD.QuestBot.Quests;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1;

public class Step3SecondaryPurpose : Step3PurposeBase
{
    public Step3SecondaryPurpose(IQuest parentQuest, QuestContext context) : base(parentQuest, context)
    {
    }

    public override string Title => "Identify your secondary purpose for joining the Discord";
    public override string Description => "Choose one of the Emojis below.";
}