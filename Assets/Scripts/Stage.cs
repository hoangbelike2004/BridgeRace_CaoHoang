using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] GameObject brick;
    [SerializeField] List<GameObject> bricks;
    [SerializeField] List<Vector3> ListBrickPos;
    [SerializeField] int values = 100;
    [SerializeField] int valueInstantiazZ;
    [SerializeField] int valueInstantiazX;
    [SerializeField] StartPoint StartPoi;
    public bool checkColorPlayerFromStart = false;
    [SerializeField] Character character;
    [SerializeField] Stage stage;
    [SerializeField] private GameObject BrickStage;

    public void SetCharacter(Character character)
    {
        this.character = character;
    }
    public void SetStage(Stage stage) {
    this.stage = stage;
    }
    //public StartPoint startPoint;

    // Start is called before the first frame update
    private void Awake()
    {
        for (int i = 0; i < values; i++)
        {
            GameObject prefabbrick = Instantiate(brick);
            prefabbrick.transform.SetParent(transform);
            prefabbrick.SetActive(false);
            bricks.Add(prefabbrick);
        }
        for (int i = -valueInstantiazZ; i < valueInstantiazZ; i += 3)
        {
            for (int j = -valueInstantiazX; j < valueInstantiazX; j += 3)
            {

                Vector3 brickPos = new Vector3(i + 1.5f + transform.position.x, transform.position.y + 0.7f, transform.position.z + j);
                ListBrickPos.Add(brickPos);
            }

        }
    }
    public void SetBrick()
    {

        for (int i = 0; i < bricks.Count; i++)
        {
            //bricks[i].SetActive(true);
            bricks[i].transform.position = ListBrickPos[i];
        }

    }

    private void ActiveBrick(int value)
    {

        for (int i = 0; i < bricks.Count; i++)
        {
            if (value == (int)bricks[i].GetComponent<Brick>().colorType)
            {
                bricks[i].SetActive(true);
            }
        }
    }
    private void OnEnable()
    {
        StartPoint._ActiveBrickEvent += ActiveBrick;
    }
    private void OnDisable()
    {
        StartPoint._ActiveBrickEvent -= ActiveBrick;
    }
    //void Start()
    //{
    //    SetBrick();
    //    Invoke(nameof(ActiveBrick), 1f);
    //}
    private IEnumerator Start()
    {
        SetBrick();
        while (true)
        {
            Debug.Log(checkColorPlayerFromStart);
            if (character != null)
            {
                for (int i = 0; i < bricks.Count; i++)
                {
                    if (character.GetComponent<Player>().colorType == bricks[i].GetComponent<Brick>().colorType)
                    {
                        bricks[i].SetActive(true);
                    }
                }
                character = null;
            }    
            else if (checkColorPlayerFromStart && character!= null)
            {
                
                for (int i = 0; i < bricks.Count; i++) {

                    if (character.GetComponent<Player>().colorType == transform.GetChild(i).GetComponent<Brick>().colorType && BrickStage.transform.GetChild(i).gameObject.activeSelf==false) {
                        BrickStage.transform.GetChild(i).gameObject.SetActive(true);
                    }
                }
                //Debug.Log(1);
                character = null;
                checkColorPlayerFromStart = false;
            }
            Debug.Log(1);
            yield return null;
            //}
        }



        


    }
}
