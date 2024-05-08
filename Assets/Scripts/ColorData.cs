using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ColorData")]
public class ColorData : ScriptableObject
{

    public Material[] materials;
    public bool[] danhdau = new bool[10];

    public Material GetColor(ColorType colortype)
    {
        return materials[(int)colortype];//tra ve ColorType
    }

    public ColorType randomColor()//general use for player and bot
    {
        int count = 0;
    lặp:
        int index = Random.Range(1, materials.Length - 1);

        if (danhdau[index] == false) //MÀU CHƯA DÙNG
        {

            danhdau[index] = true; /// ĐÁNH DẤU LẠI MÀU ĐÃ DÙNG
            return (ColorType)index;
        }
        else /// danhdau[index] == true
        {
            count++;
            if (count <= 10)  
                goto lặp;
        }
        return (ColorType)0;
    }


    public void ResetColorData()
    {
        for (int i = 0; i < 10; i++)
        {
            danhdau[i] = false;
        }
    }

}
public enum ColorType
{
    none,
    red,
    green,
    blue,
    yellow,
    black,
    violet
}