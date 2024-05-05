using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class StartPoint : MonoBehaviour
{
    public int valueColorPlayerFromStart;
    public  delegate void DeleActiveBrick(int a);
    public static DeleActiveBrick _ActiveBrickEvent;

    public Stage stage;

    public List<GameObject> others = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            valueColorPlayerFromStart =  (int)other.gameObject.GetComponent<Player>().colorType;
            //_ActiveBrickEvent?.Invoke(valueColorPlayerFromStart);
            stage.SetCharacter(other.GetComponent<Character>());
            stage.SetStage(transform.GetComponent<Stage>());
            transform.gameObject.SetActive(false);
            //others.Add(other.gameObject);

        }
    }
}