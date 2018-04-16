using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : EnemyController
{

    public enum FSMState
    {
        None,
        Patrol,
        Chase,
        //    Attack
    }


    //movement location
    protected Vector3 destPos;

    public FSMState curState = FSMState.Patrol;

    protected override void FSMStart()
    {
        curState = FSMState.Patrol;
        FindNextPoint();
    }

    protected override void FSMUpdate()
    {
        switch (curState)
        {
            case FSMState.Patrol: UpdatePatrolState(); break;
            case FSMState.Chase: UpdateChaseState(); break;
            //   case FSMState.Attack: UpdateAttackState(); break;
            default: UpdatePatrolState(); break;
        }
    }

    private void UpdateAttackState()
    {
        // throw new NotImplementedException();
    }

    private void UpdateChaseState()
    {
        print("Starting the chase");
        //  transform.LookAt(player.transform);
        //transform.Translate(0, 0, movementSpeed * Time.deltaTime);
        UpdateTarget(player.transform.position);
        if (!playerInRange())
        {
            curState = FSMState.Patrol;
        }
    }

    private void UpdatePatrolState()
    {
        if (Vector3.Distance(this.transform.position, destPos) <= 100)
        {
            print("patroll to next point");
            FindNextPoint();
        }

        //tempmovement UpdateTarget(destPos);
        UpdateTarget(destPos);
        if (player != null)
            if (playerInRange())
            {
                print("Player detected");
                curState = FSMState.Chase;
            }
    }

    protected void FindNextPoint()
    {
        float x;
        float y;
        float z;
        x = Random.Range(this.transform.position.x - 400, this.transform.position.x + 400);
        y = 0;
        z = Random.Range(this.transform.position.z - 400, this.transform.position.z + 400);
        destPos = new Vector3(x, y, z);

        //     GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //   cube.transform.position = destPos;
    }

    protected bool IsInCurrentRange(Vector3 pos)
    {
        float xPos = Mathf.Abs(pos.x - transform.position.x);
        float zPos = Mathf.Abs(pos.z - transform.position.z);

        if (xPos <= 50 && zPos <= 50)
            return true;

        return false;
    }

    private bool playerInRange()
    {
        if (player == null)
            return false;

        if (Vector3.Distance(transform.position, player.transform.position) < detectionRange)
        {
            print("Player in range");
            return true;
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            print("Killed the player");
            GameOver(player);
        }
    }

    public void GameOver(GameObject player)
    {
        Destroy(player);
    }

    void UpdateTarget(Vector3 targetPosition)
    {
        navAgent.SetDestination(targetPosition);
    }
}