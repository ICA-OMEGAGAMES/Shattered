using UnityEngine;

[CreateAssetMenu (menuName="PluggableAI/Actions/Dodge")]
public class DodgeAction : Action
{
    public override void Act(AIManager manager)
    {
        Dodge(manager);
    }

    private void Dodge(AIManager manager)
    {
        if (!manager.IsCooldownExpired())
        {    
            return;
        }       
        
        if((Vector3.Distance(manager.transform.position, manager.GetTargetPosition()) 
                <= (manager.aiStats.reachedDistance * manager.aiStats.reachedTollerance)) 
                && manager.Dodge((UnityEngine.Random.Range(0.0f, 5.0f) >= 4))){
            manager.SetAttackCooldown(manager.aiStats.dodgeCooldown);
        }
    }
}