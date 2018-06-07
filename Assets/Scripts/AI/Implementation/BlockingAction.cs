
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Blocking")]
public class BlockingAction : Action
{
    public override void Act(AIManager manager)
    {
        Block(manager);
    }

    private void Block(AIManager manager)
    {
        if (!manager.IsCooldownExpired() || manager.IsAttackTimestampSet())
        {
            return;
        }

        if (manager.Block(UnityEngine.Random.Range(0.0f, 10.0f) >= 9, manager.aiStats.unarmedCombatSettings.blockDuration))
        {
            // block based on chance
            manager.SetAttackCooldown(manager.aiStats.unarmedCombatSettings.blockDuration);
        }
    }
}