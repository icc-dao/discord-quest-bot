using ICCD.QuestBot.Quests;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1;

public class Step3SkillsContractor : BaseQuestStep
{
    public Step3SkillsContractor(IQuest parentQuest, QuestContext context) : base(parentQuest, context)
    {
    }

    public override string Title => "Your Creative Expertise demand";

    public override string Description => @"Choose the creative expertise you are interested in contracting by clicking the Emojis below.";
}