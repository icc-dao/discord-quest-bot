using ICCD.QuestBot.Quests;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step2;

public class Step2DaoPrinciples1 : Step2DaoPrinciplesBase
{
    public Step2DaoPrinciples1(IQuest parentQuest, QuestContext context) : base(parentQuest, context)
    {
    }

    public override string Title => "Read and acknowledge core DAO principles.";
    public override string Description => @"**__About Us__ (1/5)**
The IOTA Content Creators DAO (ICCD) is a decentralized movement of people who are looking to contribute to the IOTA ecosystem.

The ICCD is open to everyone, whether you have technical skills or not, to connect, contribute, and grow exciting projects powered by IOTA.";
}