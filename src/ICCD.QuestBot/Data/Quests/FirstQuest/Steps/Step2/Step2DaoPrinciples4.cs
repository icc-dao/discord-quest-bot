using ICCD.QuestBot.Quests;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step2;

public class Step2DaoPrinciples4  : Step2DaoPrinciplesBase
{
    public Step2DaoPrinciples4(IQuest parentQuest, QuestContext context) : base(parentQuest, context)
    {
    }

    public override string Title { get; }
    public override string Description => @"**__Values (4/5)__**
**1. Integrity:** We operate transparently and build trust through open and fair processes to facilitate creators and clients alike.
**2. Accessibility:** We aim to make building on IOTA accessible for everyone who has skills they’re willing to offer. No longer is crypto gated exclusively for software developers and investors.
**3. Decentralization:** We believe in a strong community and an environment where the best ideas win. All major decisions are voted on by members of the DAO.
**4. Taking Initiative:** We reward action and embrace risk. We empower our community to continually drive new initiatives by providing a space to self-organize and quickly move from idea to action.";
}