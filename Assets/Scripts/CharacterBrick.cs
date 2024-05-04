using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBrick : MonoBehaviour
{
    public ColorType colorType;
    [SerializeField] public Renderer meshRen;
    [SerializeField] protected ColorData colordata;
    protected void ChangeColor(ColorType ecolor)
    {
        colorType = ecolor;
        meshRen.material = colordata.GetColor(colorType);
    }



}
