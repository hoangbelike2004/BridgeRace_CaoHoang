using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PartrolState : IState
{
    //int valuesbrick;
    
    Vector3 distancetobridge;
    bool backtobricks;
    List<Transform> transformsBrick;
    bool isStart,isContinueMove;
    public void OnEnter(BotAi botai)
    {
        //valuesbrick = 0;
        
        Debug.Log(botai.maxvaluesbick);
        distancetobridge = Vector3.zero;
        backtobricks = true;
        Debug.Log("Maxvalues: " + botai.maxvaluesbick);
        Vector3 newPos = botai.stage.GetComponent<Stage>().GetPosBrick(botai.GetComponent<Character>().colorType);
        botai.target = newPos;
        if (botai.target == newPos)
        {
            isStart = false;
        }
        botai.MoveBot();
        //for (int i = 0; i < botai.stage.bricks.Count; i++)
        //{
        //    if (botai.transform.GetComponent<Character>().colorType == botai.stage.bricks[i].GetComponent<Brick>().colorType)
        //    {
        //        transformsBrick.Add(botai.stage.bricks[i].GetComponent<Brick>().transform);
        //    }
        //}
        //botai.target = transformsBrick[Random.RandomRange(0, transformsBrick.Count)].position;
        //Debug.Log("Count: "+transformsBrick.Count);
    }



    public void OnExcute(BotAi botai)
    {
        if (botai.stage != null&&isStart)
        {
            
            for (int i = 0; i < botai.stage.bricks.Count; i++)
            {
                if (botai.transform.GetComponent<Character>().colorType == botai.stage.bricks[i].GetComponent<Brick>().colorType)
                {
                    if (botai.stage.bricks[i].gameObject.activeSelf == true)
                    {
                        if(botai.bricks.Count < botai.maxvaluesbick &&backtobricks)
                        {
                            //if(botai.target == Vector3.zero)
                            //{
                            if(botai.bricks.Count == 0)
                            {
                             botai.target = botai.stage.GetComponent<Stage>().GetPosBrick(botai.GetComponent<Character>().colorType);
                            }
                            else//partrol addbrick
                            {
                                botai.target = botai.stage.bricks[i].transform.position;
                            }
                            

                            
                            
                            
                            if (Vector3.Distance(botai.transform.position, botai.stage.bricks[i].transform.position) < .1f)
                            {
                                //valuesbrick++;
                                Debug.Log(botai.bricks.Count);
                                botai.target = Vector3.zero;
                                
                            }
                        }
                        else if(botai.bricks.Count == botai.maxvaluesbick)
                        {
                            backtobricks = false;
                            //Debug.Log("bridge1");
                           //int a = Random.Range(0, botai.stage.bridges.Count-1);
                           botai.target = botai._finish.position;
                            //Debug.Log(botai.target);
                        }
                        if(botai.bricks.Count == 0&&botai.onBridge)
                        {
                            backtobricks = true;
                            botai.maxvaluesbick = Random.Range(7, 11);
                            Debug.Log("Maxvalues: "+botai.maxvaluesbick+botai.name);
                          
                            //Debug.Log("numberone: " + botai.target);
                        }
                        
                        
                        //Debug.Log(botai.target);
                        break;
                    }

                }
            }
            //stage = null;
        }
        //GetBrickPos();
        
        botai.MoveBot();

    }



    public void OnExit(BotAi botai)
    {

    }
}
