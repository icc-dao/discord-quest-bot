using ICCD.QuestBot.Quests;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1;

public class Step3SkillsContentCreatorPrimary : BaseQuestStep
{
    public Step3SkillsContentCreatorPrimary(IQuest parentQuest, QuestContext context) : base(parentQuest, context)
    {
    }

    public override string Title => "Your Main Creative Expertises";

    public override string Description => @"Choose your main creative expertise by clicking one of the Emojis below.
You will be displayed as a member of this expertise in the member list and receive notifications from pings on your expertise.";
}