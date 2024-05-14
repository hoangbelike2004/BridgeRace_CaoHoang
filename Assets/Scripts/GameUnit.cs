using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolType {brick,map,bot };
public abstract class GameUnit : MonoBehaviour
{
    private Transform tf;
    public Transform Tf
    {
        get
        {
            if(tf == null)
            {
                tf = gameObject.transform;
            }
            return tf;
        }
    }
    public PoolType poolType;
}
