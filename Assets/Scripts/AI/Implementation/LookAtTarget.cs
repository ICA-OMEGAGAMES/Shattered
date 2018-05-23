using UnityEngine;

[CreateAssetMenu (menuName="PluggableAI/Actions/Look At Target")]
public class LookAtTarget : Action
{
    public override void Act(AIManager manager)
    {
        Look(manager);
    }

    private void Look(AIManager manager)
    {
        manager.transform.LookAt(manager.GetTargetPosition());
    }
}