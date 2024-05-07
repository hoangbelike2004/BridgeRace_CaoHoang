using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BotAi : Character
{
    private IState CurrentState;
    [SerializeField] private NavMeshAgent _NavMeshAgent;
    private List<Brick> brickListbot;
    public Vector3 target;
    private void Update()
    {
        if(CurrentState != null)
        {
            CurrentState.OnExcute(this);
        }
    }
    public void ChangeState(IState Newstate)
    {
        if (CurrentState != null)
        {
            CurrentState.OnExit(this);
        }
        CurrentState = Newstate;
        if (Newstate != null)
        {
            CurrentState.OnEnter(this);
        }
    }
    public void MoveBot()
    {
        _NavMeshAgent.SetDestination(target);
    }
    protected override void OnInit()
    {
        base.OnInit();
        for(int i = 0; i< brickChild.childCount; i++)
        {
            
        }
    }
    protected override void Ondespawn()
    {
        base.Ondespawn();
    }
}
