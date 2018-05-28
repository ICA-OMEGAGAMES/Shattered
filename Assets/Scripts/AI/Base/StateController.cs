using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AIManager))]
[RequireComponent(typeof(AIAnimationManager))]
public class StateController : MonoBehaviour
{
    public State currentState;
    public State remainState;

    [HideInInspector] public AIManager manager;
    [HideInInspector] public AIAnimationManager animationManager;
    [HideInInspector] public State previousState;

    private bool aiActive;

    void Start()
    {
        manager = GetComponent<AIManager>();
        aiActive = manager.SetUpAiManager(this);
        animationManager = GetComponent<AIAnimationManager>();
        animationManager.SetUpAIAnimationManager();
    }

    void Update()
    {
        if (!aiActive)
        {
            return;
        }
        currentState.UpdateState(this, manager);
        animationManager.Animate(manager.navMeshAgent.velocity.magnitude);
    }
    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            previousState = currentState;
            currentState = nextState;
            OnExitState();
        }
    }

    public void Die()
    {
        aiActive = false;
    }

    private void OnExitState()
    {
        manager.StopMovement();
        if (manager.IsCombatEnabled())
        {
            manager.SwitchCombatState(false);
        }
    }
}