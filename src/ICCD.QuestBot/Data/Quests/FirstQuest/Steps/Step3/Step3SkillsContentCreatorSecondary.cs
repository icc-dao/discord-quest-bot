using ICCD.QuestBot.Quests;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1;

public class Step3SkillsContentCreatorSecondary : BaseQuestStep
{
    public Step3SkillsContentCreatorSecondary(IQuest parentQuest, QuestContext context) : base(parentQuest, context)
    {
    }

    public override string Title => "Your Other Creative Expertises";

    public override string Description => @"Choose the creative expertises you would like to also be notified for by clicking the emojis below.";
}