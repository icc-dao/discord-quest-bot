using ICCD.QuestBot.Quests;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step2;

public class Step2DaoPrinciples2 : Step2DaoPrinciplesBase
{
    public Step2DaoPrinciples2(IQuest parentQuest, QuestContext context) : base(parentQuest, context)
    {
    }

    public override string Title { get; }
    public override string Description => @"**__Mission Statement (2/5)__**
We aim to be the engine that facilitates community growth and projects within the IOTA ecosystem. 🚀";
}