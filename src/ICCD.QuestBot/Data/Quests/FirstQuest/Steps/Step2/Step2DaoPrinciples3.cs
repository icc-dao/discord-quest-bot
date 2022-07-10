using ICCD.QuestBot.Quests;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step2;

public class Step2DaoPrinciples3  : Step2DaoPrinciplesBase
{
    public Step2DaoPrinciples3(IQuest parentQuest, QuestContext context) : base(parentQuest, context)
    {
    }

    public override string Title { get; }
    public override string Description => @"**__Vision (3/5)__**
To live in a world where Web3 is feeless and not gated exclusively to those who are willing to pay transaction fees. Building a robust IOTA ecosystem enables this.";
}