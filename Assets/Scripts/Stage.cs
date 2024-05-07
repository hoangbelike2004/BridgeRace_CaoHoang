using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Stage : CharacterBrick
{
    [SerializeField] GameObject brick;
    [SerializeField] List<GameObject> bricks;
    [SerializeField] List<Vector3> ListBrickPos;
    [SerializeField] int values = 100;
    [SerializeField] int valueInstantiazZ;
    [SerializeField] int valueInstantiazX;
    [SerializeField] StartPoint StartPoi;
    public bool checkColorPlayerFromStart = false;
    [SerializeField] List<Character> characters = new List<Character>(6);
    [SerializeField] Stage stage;
    //[SerializeField] private GameObject BrickStage;
    public bool isStart;

    public void SetCharacter(Character character)
    {
        //this.character = character;

        bool isCharacter = true;
        for (int i = 0; i < characters.Count; i++) {
            if (characters[i] == character) {
                isCharacter = false;
                break;
            }    
        }
        //Debug.Log(isCharacter);
        if (isCharacter) {
            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i] == null)
                {
                    characters[i] = character;
                    //Debug.Log("chay");
                    break;
                }

            }
            //characters.Add(character);
           // Debug.Log("chay");
        }
    }
    public void SetStage(Stage stage) {
    this.stage = stage;
        Debug.Log(1);
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
            
            if (characters != null) {
                for (int c = 0; c < characters.Count; c++)
                {
                    //Debug.Log(characters[c].gameObject.name);
                    //Debug.Log(checkColorPlayerFromStart);
                    if (characters[c] != null && isStart&&stage!= null)
                    {
                        for (int i = 0; i <bricks.Count; i++)
                        {
                            if (characters[c].GetComponent<Character>().colorType == stage.bricks[i].GetComponent<Brick>().colorType)
                            {
                                stage.bricks[i].gameObject.SetActive(true);
                            }
                        }
                        characters[c] = null;
                        isStart = false;
                       

                    }
                    //Debug.Log(1);

                    //}
                }

                for (int c = 0; c < characters.Count; c++)
                {
                    if (checkColorPlayerFromStart && characters[c] != null)
                    {
                        Debug.Log(1);
                        int colorindex = (int)characters[c].GetComponent<Character>().colorType;
                        int valuesBricks = Random.Range(0, bricks.Count - 1);
                        if (stage.transform.GetChild(valuesBricks+3).gameObject.activeSelf == false)
                        {
                            
                            bricks[valuesBricks].GetComponent<Brick>().meshRen.material = colordata.materials[colorindex];
                            bricks[valuesBricks].GetComponent<Brick>().colorType = (ColorType)colorindex;
                        }





                        for (int i = 3; i < bricks.Count; i++)
                        {

                            if (characters[c].GetComponent<Character>().colorType == transform.GetChild(i).GetComponent<Brick>().colorType && stage.transform.GetChild(i).gameObject.activeSelf == false)
                            {
                                stage.transform.GetChild(i).gameObject.SetActive(true);
                            }
                            else if (characters[c].GetComponent<Character>().colorType != transform.GetChild(i).GetComponent<Brick>().colorType)
                            {
                                stage.transform.GetChild(i).gameObject.SetActive(false);
                            }
                        }

                        characters[c] = null;
                        //checkColorPlayerFromStart = false;
                    }
                    //Debug.Log(characters.Count);
                    
                    
                }
                    
            }




            else { yield return null; }

            //Debug.Log(1);
            yield return null;
            //}

        }
    }
}
