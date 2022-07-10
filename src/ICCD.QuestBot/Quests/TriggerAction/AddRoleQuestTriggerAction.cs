using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using ICCD.QuestBot.Quests.Interfaces;

namespace ICCD.QuestBot.Quests.TriggerAction;

/// <summary>
/// A trigger action that adds or removes a role to or from a user.
/// </summary>
public class AddRoleTriggerAction : IQuestTriggerAction
{
    private readonly string? _roleName;
    private readonly ulong? _roleId;
    private IRole _role;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddRoleTriggerAction"/> class.
    /// </summary>
    /// <param name="roleId">The ID of the role.</param>
    public AddRoleTriggerAction(ulong roleId)
    {
        _roleId = roleId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AddRoleTriggerAction"/> class.
    /// </summary>
    /// <param name="roleName">The name of the role.</param>
    public AddRoleTriggerAction(string roleName)
    {
        _roleName = roleName;
    }

    /// <inheritdoc />
    public async Task ExecuteUp(QuestContext questContext)
    {
        Init(questContext);
        await questContext.SocketGuildUser.AddRoleAsync(_role);
    }

    /// <inheritdoc />
    public async Task ExecuteDown(QuestContext questContext)
    {
        Init(questContext);
        await questContext.SocketGuildUser.RemoveRoleAsync(_role);
    }

    private Task Init(QuestContext context)
    {
        if (_roleId.HasValue)
        {
            _role = context.SocketGuildUser.Guild.GetRole(_roleId.Value) ??
                    throw new ApplicationException($"Role with ID {_roleId} is not valid.");
        }
        else
        {
            _role = context.SocketGuildUser.Guild.Roles.Single(x =>
                x.Name.Equals(_roleName, StringComparison.InvariantCultureIgnoreCase));
        }

        return Task.CompletedTask;
    }
}