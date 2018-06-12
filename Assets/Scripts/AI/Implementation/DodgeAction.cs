
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Dodge")]
public class DodgeAction : Action
{
    private float dodgingTimestamp = 0;
    public override void Act(AIManager manager)
    {
        Dodge(manager);
    }

    private void Dodge(AIManager manager)
    {
        if (!manager.IsCooldownExpired() || manager.IsAttackTimestampSet())
        {
            return;
        }

        if (Time.time > dodgingTimestamp && (Vector3.Distance(manager.transform.position, manager.GetTargetPosition())
                <= (manager.aiStats.movementStats.reachedDistance * (1 + manager.aiStats.movementStats.reachedTollerance)))
                && manager.Dodge(UnityEngine.Random.Range(0.0f, 5.0f) >= 3))
        {
            //if the player is near, dodge based on chance
            manager.SetAttackCooldown(manager.aiStats.movementStats.dodgeCooldown);
            dodgingTimestamp = Time.time + manager.aiStats.movementStats.dodgeLock;
        }
    }
}