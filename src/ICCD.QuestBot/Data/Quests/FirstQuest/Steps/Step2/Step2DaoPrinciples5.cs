using ICCD.QuestBot.Quests;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step2;

public class Step2DaoPrinciples5  : Step2DaoPrinciplesBase
{
    public Step2DaoPrinciples5(IQuest parentQuest, QuestContext context) : base(parentQuest, context)
    {
    }

    public override string Title { get; }
    public override string Description => @"**__Code of Ethics (5/5)__**
**1. Be respectful:** Treat other community members how you would like to be treated.
**2. Be real:** We have zero tolerance for scammers, swindlers, and charlatans in our community.
**3. Builder & Client Satisfaction:** We take pride in making sure everyone who interacts with the ICCD, as either a builder or customer, has a good experience with the DAO and strive for it with every interaction.";
}