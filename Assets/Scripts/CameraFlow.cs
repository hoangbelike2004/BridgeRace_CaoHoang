using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    [SerializeField] private Transform Player;
    private void LateUpdate()
    {
        transform.position = new Vector3(Player.position.x, Player.position.y+30,Player.position.z-21.6f);
    }
}
