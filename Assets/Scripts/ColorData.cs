using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ColorData")]
public class ColorData : ScriptableObject
{

    public Material[] materials;
    public Material GetColor(ColorType colortype)
    {
        return materials[(int)colortype];//tra ve ColorType
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