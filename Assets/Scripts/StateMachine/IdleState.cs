using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float time = 0f;
    float timer = 3f;

    public void OnEnter(BotAi botai)
    {
        
    }

   

    public void OnExcute(BotAi botai)
    {
        time += Time.deltaTime;
        if(time > timer)
        {
            botai.ChangeAnim(Character.animationState.idle);
            botai.ChangeState(new PartrolState());
            time = 0f;
        }
    }



    public void OnExit(BotAi botai)
    {
        
    }
}
