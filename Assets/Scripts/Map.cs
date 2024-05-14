using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : Singleton<Map>
{
    [SerializeField] int valuesList;
    public List<Transform> _listStartPoint = new List<Transform>(3);
    //bool[] danhdau = new bool[5];
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
    //public void ResetColorData()
    //{
    //    for (int i = 0; i < 5; i++)
    //    {
    //        danhdau[i] = false;
    //    }
    //}
}
