using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : CharacterBrick
{

    
    [SerializeField] private float timeActive;
    private bool isDiactive;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"&& this.colorType == other.gameObject.GetComponent<Character>().colorType)
        {
            transform.gameObject.SetActive(false);
            //int colorindex = Random.Range(0, colordata.materials.Length - 1);
            //meshRen.material = colordata.materials[colorindex];
            //colorType = (ColorType)colorindex;
            isDiactive = true;
            //if (isDiactive)
            //{
            //    Debug.Log("Active");
            //    Invoke(nameof(ActiveBrick), timeActive);
            //}
        }
    }
    //private void Start()
    //{
    //    meshRen.material = colordata.materials[Random.Range(0,colordata.materials.Length-1)];

    //}

    private void Awake()
    {
        //Debug.Log(colordata.materials.Length);
        isDiactive = false;
        int colorindex = Random.Range(0, colordata.materials.Length - 1);
        meshRen.material = colordata.materials[colorindex];
        colorType = (ColorType)colorindex;

    }
    //void ActiveBrick()
    //{

    //    //Debug.Log(isDiactive);
    //    //yield return new WaitForSeconds(timeActive);
    //    //int colorindex = Random.Range(1, colordata.materials.Length - 1);
    //    //meshRen.material = colordata.materials[colorindex];
    //    //this.colorType = (ColorType)colorindex;
    //    transform.gameObject.SetActive(true);
    //    isDiactive = false;

    //}

}
