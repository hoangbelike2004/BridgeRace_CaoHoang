using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    
    public  delegate void DelegateGameWin();
    public static DelegateGameWin WinEvent;
    public Transform _transformFinish;
    //public List<Transform> _listStartPoint = new List<Transform>(3);
    
    public delegate void DelegateGetvalueLevel();
    public static DelegateGetvalueLevel GetvalueLevelEvent;
    bool[] danhdau = new bool[5];
    public bool isFinish;

    //public Vector3 Randomtransform()//general use for player and bot
    //{
    //    int count = 0;
    //lặp:
    //    int index = Random.Range(1, _listStartPoint.Count);

    //    if (danhdau[index] == false) //MÀU CHƯA DÙNG
    //    {

    //        danhdau[index] = true; /// ĐÁNH DẤU LẠI MÀU ĐÃ DÙNG
    //        return _listStartPoint[index].position;
    //    }
    //    else /// danhdau[index] == true
    //    {
    //        count++;
    //        if (count <= 5)
    //            goto lặp;
    //    }
    //    return Vector3.zero;
    //}
    public void GameLose()
    {

    }
    public void GameWin()//sau khi win player se goi den gamewin và gamewin se phat di 1 tin hieu de hien UI Win
    {
        WinEvent?.Invoke();
        isFinish = true;
    }
    public void LoadGame()
    {
        GameObject newGameObject = GameObject.Find("FinishBox");
        _transformFinish = newGameObject.transform;
       
        GetvalueLevelEvent?.Invoke();
    }
    
    private void OnEnable()
    {
        UIManagerSystem.PlayGameEvent += LoadGame;
       // UIManagerSystem.WinGameEvent += LoadGame;
    }
    private void OnDisable()
    {
        UIManagerSystem.PlayGameEvent -= LoadGame;
       // UIManagerSystem.WinGameEvent += LoadGame;
    }
    
  
}
