using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/State")]
public class State : ScriptableObject
{

    public Action[] actions;
    public Transition[] transitions;

    public void UpdateState(StateController controller, AIManager manager)
    {
        DoActions(manager);
        CheckTransitions(controller, manager);
    }

    private void DoActions(AIManager manager)
    {
        Array.ForEach(actions, item =>
        {
            if (item != null)
            {
                item.Act(manager);
            }
        });
    }

    private void CheckTransitions(StateController controller, AIManager manager)
    {
        Array.ForEach(transitions, item =>
        {
            bool decisionSucceeded = item.decision.Decide(manager);

            if (decisionSucceeded)
            {
                controller.TransitionToState(item.trueState);
            }
            else
            {
                controller.TransitionToState(item.falseState);
            }
        });
    }


}