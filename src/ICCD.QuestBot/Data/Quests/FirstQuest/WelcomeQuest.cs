using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1;
using ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step1.Triggers;
using ICCD.QuestBot.Data.Quests.FirstQuest.Steps.Step2;
using ICCD.QuestBot.Quests;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Data.Quests.FirstQuest;

public class WelcomeQuest : BaseQuest
{
    private const ulong _userIdPathin = 189498611690766336;
    private const ulong _helpChannel = 953692240658710528;

    private const ulong _channelIdIntroduceYourself = 936702185356558396;
    private const ulong _channelIdCommunityHighlights = 918444447044362241;
    private const ulong _channelIdEvents = 918451405650530324;
    private const ulong _channelIdGeneral = 918246591587041283;
    private const ulong _channelIdServices = 994198799624962048;
    private const ulong _channelIdJobs = 936773350812962917;
    private const ulong _channelIdPitchDeck = 939207469626982530;

    public WelcomeQuest(QuestContext context) : base(context)
    {
    }

    public override string Id => "first-quest";
    public override string Title => "Welcome to the ICCD";

    public override string Description => @$"Hello @{{user}}, welcome to the IOTA Content Creator DAO.
To give you the best DAO experience possible we have prepared a starter quest for you.
Please finish this quest to access the most active channels in our Discord.

**If you have trouble finishing the Quest, please contact <@{_userIdPathin}> or search help in the <#{_helpChannel}> channel!**";

    public string QuestMessageSuccess
    {
        get
        {
            var sb = new StringBuilder();

            sb.AppendLine("Thank you for completing the quest, please take a look at some of the great channels we have below to get started on your ICCD journey.");

            sb.AppendLine($"<#{_channelIdIntroduceYourself}>");
            sb.AppendLine($"<#{_channelIdCommunityHighlights}>");
            sb.AppendLine($"<#{_channelIdEvents}>");
            sb.AppendLine($"<#{_channelIdGeneral}>");
            sb.AppendLine($"<#{_channelIdServices}>");

            if (QuestSteps.SelectMany(x => x.Triggers.Where(x => x.Status == IQuestTrigger.TriggerStatus.Triggered))
                .Any(x => x.GetType() == typeof(TriggerPurposeContractor)))
            {
                sb.AppendLine($"<#{_channelIdJobs}>");
            }

            if (
                QuestSteps.SelectMany(x => x.Triggers.Where(x => x.Status == IQuestTrigger.TriggerStatus.Triggered))
                .Any(x => x.GetType() == typeof(TriggerPurposeContentCreator)))
            {
                sb.AppendLine($"<#{_channelIdPitchDeck}>");
            }

            return sb.ToString();
        }
    }

    public override Type StartingStepType { get; } = typeof(Step1SafetyGuidelines);

    public override Task Finish()
    {
        if (this.QuestStatus.Code == StatusCode.Succeeded)
        {
            this.QuestStatus.Success(QuestMessageSuccess);
        }
        return base.Finish();
    }

    public override Task ResetAction()
    {
        var stepTypes = new HashSet<Type>{
            typeof(Step1SafetyGuidelines),
            typeof(Step2DaoPrinciples1),
            typeof(Step2DaoPrinciples2),
            typeof(Step2DaoPrinciples3),
            typeof(Step2DaoPrinciples4),
            typeof(Step2DaoPrinciples5),
            typeof(Step3PrimaryPurpose),
            typeof(Step3SecondaryPurpose),
            typeof(Step3SkillsContentCreatorPrimary),
            typeof(Step3SkillsContentCreatorSecondary),
            typeof(Step3SkillsContractor),
            typeof(Step3PurposeBase)
        };

        var allTriggerTypes = typeof(IQuestTrigger<>).Assembly.GetTypes()
                    .Where(t => !t.IsAbstract && !t.IsInterface && t.GetInterfaces().Any(x =>
                        x.IsGenericType && (x.GetGenericTypeDefinition() == typeof(IQuestTrigger<>)))).Where(x =>
                    x.BaseType.IsGenericType && (stepTypes.Contains(x.BaseType.GetGenericArguments()![0])));

        foreach (var triggerType in allTriggerTypes)
        {
            var instance = Activator.CreateInstance(triggerType, new object[] { }) as IQuestTrigger;
            if (instance != null)
            {
                foreach (var action in instance.QuestTriggerActions)
                {
                    action.ExecuteDown(Context);
                }
            }
        }

        return base.ResetAction();
    }
}