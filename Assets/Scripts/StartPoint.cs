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
            valueColorPlayerFromStart =  (int)other.gameObject.GetComponent<Character>().colorType;
            //_ActiveBrickEvent?.Invoke(valueColorPlayerFromStart.3f);
            stage.SetCharacter(other.GetComponent<Character>());
            //Debug.Log(other.gameObject.name);
            stage.isStart = true;
            //Debug.Log(other.gameObject.name);
            Invoke(nameof(DeActiveStartPoint), 0.3f);
            
            //others.Add(other.gameObject);

        }
        if (other.CompareTag("Bot"))
        {
            valueColorPlayerFromStart = (int)other.gameObject.GetComponent<Character>().colorType;
            //_ActiveBrickEvent?.Invoke(valueColorPlayerFromStart.3f);
            stage.SetCharacter(other.GetComponent<Character>());
            //Debug.Log(other.gameObject.name);
            stage.isStart = true;
            //Debug.Log(other.gameObject.name);
            Invoke(nameof(DeActiveStartPoint), 0.3f);

            //others.Add(other.gameObject);

        }
    }

    private void DeActiveStartPoint()
    {
        transform.gameObject.SetActive(false);
    }
    //private void OnEnable()
    //{
    //    UIManagerSystem.LoseGameEvent += ActiveStartPoint;
    //}

    //private void OnDisable()
    //{
    //    UIManagerSystem.LoseGameEvent -= ActiveStartPoint;
    //}
    //private void ActiveStartPoint()
    //{
    //    transform.gameObject.SetActive(true);
    //}
}
