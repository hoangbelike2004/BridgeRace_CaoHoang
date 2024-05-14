using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Goupthebridge : IState
{
    //int valuesbrick;
    
    Vector3 distancetobridge;
    List<Transform> transformsBrick;
    bool isStart,isContinueMove;
    public void OnEnter(BotAi botai)
    {
       //botai.target = botai._finish.position;
    }



    public void OnExcute(BotAi botai)
    {
        if (botai.bricks.Count == botai.maxvaluesbick)//move up  bridge
        {
  
            //Debug.Log("bridge1");
            //int a = Random.Range(0, botai.stage.bridges.Count-1);
            botai.target = botai._finish.position;
            //Debug.Log(botai.target);
        }
        if (botai.bricks.Count == 0 && botai.onBridge)//random brick need to pick up after running out of bricks (check when on brige and brichs.count == 0 then changestate)
        {
 
            botai.maxvaluesbick = Random.Range(7, 11);
            //Debug.Log("Maxvalues: " + botai.maxvaluesbick + botai.name);
            botai.ChangeState(new AddBrick());
            botai.onBridge = false;
            //Debug.Log("numberone: " + botai.target);
        }




        botai.MoveBot();



    }

    public void OnExit(BotAi botai)
    {

    }
}
