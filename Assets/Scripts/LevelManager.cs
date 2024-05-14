using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<GameObject> _Listlevel;
    public List<Transform> _listStartPointLevel = new List<Transform> ();
    public delegate void DelegateGetTransform();
    public static DelegateGetTransform GetTransformEvent;
    public Transform _transformFinish;
    public int valuesLevel = 0;
    //[SerializeField] int indexMap;
    private GameObject _newGameobject;
    public bool[] danhdau = new bool[10];
    public Transform Randomtransform()//general use for player and bot
    {
        int count = 0;
    lặp:
        //Debug.Log("_listStartPointLevel" + _listStartPointLevel.Count);
        int index = Random.Range(0, _listStartPointLevel.Count);


        if (danhdau[index] == false) 
        {

            danhdau[index] = true; /// ĐÁNH DẤU LẠI MÀU ĐÃ DÙNG
            //Debug.Log(_listStartPointLevel[index]);
            return _listStartPointLevel[index];
        }
        else /// danhdau[index] == true
        {
            count++;
            if (count <= 10)
                goto lặp;
        }
        return null;
    }
    private void Awake()
    {
        //int value = indexMap;
        for(int i = 0;i< 3; i++)
        {
            GameObject prefab = Resources.Load($"Map{i+1}") as GameObject;
            _Listlevel.Add(prefab);
        }
        
    }
    public void ResetTransform()
    {
        for (int i = 0; i < 10; i++)
        {
            danhdau[i] = false;
        }
    }
    public void SetLevel()
    {
        _newGameobject.SetActive(false);
        //_Listlevel[valuesLevel].gameObject.SetActive(false);
        valuesLevel++;
        _newGameobject = Instantiate(_Listlevel[valuesLevel]);
        _transformFinish = _newGameobject.transform.Find("FinishBox");
        //_Listlevel[valuesLevel].gameObject.SetActive(true);
        GameController.Instance.LoadGame();
        SetPosStartPoint();
        GetTransformEvent?.Invoke();
        
        ResetTransform();
    }
    public void PLayGameLevel1()
    {
        Debug.Log(valuesLevel);
        _newGameobject =  Instantiate(_Listlevel[valuesLevel]);
        _transformFinish = _newGameobject.transform.Find("FinishBox");
        SetPosStartPoint();
        GetTransformEvent?.Invoke();
        ResetTransform();
        
    }
    public void SetLevelAterLose()
    {
            _newGameobject.SetActive(false);
        
        //_Listlevel[valuesLevel].gameObject.SetActive(false);
        _Listlevel[valuesLevel] = null; 

        //_Listlevel[valuesLevel].gameObject.SetActive(true);
        //_Listlevel.Remove(_Listlevel[valuesLevel]);
       GameObject prefab = Resources.Load("Map"+(1+valuesLevel)) as GameObject;
        _Listlevel[valuesLevel] = prefab;

        //_Listlevel[valuesLevel].gameObject.SetActive(true);
        _newGameobject = Instantiate(_Listlevel[valuesLevel]);
        _transformFinish = _newGameobject.transform.Find("FinishBox");
        SetPosStartPoint();

        GetTransformEvent?.Invoke();
        ResetTransform();
    }
    private void OnEnable()
    {
        UIManagerSystem.WinGameEvent += SetLevel;
        //UIManagerSystem.WinGameEvent += SetPosStartPoint;
        UIManagerSystem.LoseGameEvent += SetLevelAterLose;
    }
    private void OnDisable()
    {
        UIManagerSystem.WinGameEvent += SetLevel;
        //UIManagerSystem.WinGameEvent += SetPosStartPoint;
        UIManagerSystem.LoseGameEvent += SetLevelAterLose;

    }
    public void SetPosStartPoint()
    {
        _listStartPointLevel.Clear();
        for (int i = 0; i < _Listlevel[valuesLevel].gameObject.GetComponent<Map>()._listStartPoint.Count; i++)
        {
            _listStartPointLevel.Add(_newGameobject.gameObject.GetComponent<Map>()._listStartPoint[i]);
            //Debug.Log(_newGameobject.name);
            Debug.Log(_newGameobject.gameObject.GetComponent<Map>()._listStartPoint[i].position);

        }
    }
}
