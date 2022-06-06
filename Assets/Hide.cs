using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hide : State
{
    protected Transform SafeZone;

    public Hide(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.HIDE;
        agent.speed = 5;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        SafeZone = GameObject.FindGameObjectWithTag("Safe").transform;
        anim.SetTrigger("isRunning");
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(SafeZone.position);
        if (agent.hasPath)
        {
            if(Vector3.Distance(npc.transform.position, SafeZone.position) < 1)
            {
                nextState = new Patrol(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isRunning");
        base.Exit();
    }
}
