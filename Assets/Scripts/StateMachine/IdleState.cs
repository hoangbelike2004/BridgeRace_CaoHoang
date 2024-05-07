using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float time = 0f;
    float timer = 1f;

    public void OnEnter(BotAi botai)
    {
        
    }

   

    public void OnExcute(BotAi botai)
    {
        time += Time.deltaTime;
        if(time > timer)
        {
            botai.ChangeState(new PartrolState());
        }
    }



    public void OnExit(BotAi botai)
    {
        
    }
}
