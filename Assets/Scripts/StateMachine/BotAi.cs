using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BotAi : Character
{
    private IState CurrentState;
    [SerializeField] private NavMeshAgent _NavMeshAgent;
    private List<int> valuesbrick;
    public Vector3 target;
    private Vector3 distan;
    public int maxvaluesbick;
    public Transform _finish;
    public bool onBridge;
    private void Update()
    {
        if(CurrentState != null)
        {
            CurrentState.OnExcute(this);
        }
        //if(stage != null)
        //{
        //    for(int i = 0; i < stage.bricks.Count; i++)
        //    {
        //        if(transform.GetComponent<Character>().colorType == stage.bricks[i].GetComponent<Brick>().colorType)
        //        {
        //            if (stage.bricks[i].gameObject.activeSelf == true)
        //            {
        //                target = stage.bricks[i].transform.position;
        //                if (Vector3.Distance(transform.position, stage.bricks[i].transform.position) < .1f)
        //                {
        //                    target = Vector3.zero;
        //                }
        //                Debug.Log(target);
        //                break;
        //            }
                   
        //        }
        //    }
        //    //stage = null;
        //}
        ////GetBrickPos();
        //MoveBot();
    }
    
    
    public void ChangeState(IState Newstate)
    {
        if (CurrentState != null)
        {
            CurrentState.OnExit(this);
        }
        CurrentState = Newstate;
        if (Newstate != null)
        {
            CurrentState.OnEnter(this);
        }
    }
    public void MoveBot()
    {
        
        _NavMeshAgent.SetDestination(target);
        ChangeAnim(animationState.run);
        
        
    }
    public void GetBrickPos()
    {
        if(target == Vector3.zero)
        {
            int i = Random.Range(0, valuesbrick.Count);
            target = stage.bricks[valuesbrick[i]].transform.position;
        }
        
        
    }
    protected override void OnInit()
    {

        base.OnInit();
        distan = Vector3.zero;
        _NavMeshAgent.speed = moveSpeed;
        maxvaluesbick = Random.Range(7, 11);
        ChangeState(new IdleState());
        //Debug.Log(_transforms.Count);
    }
    protected override void Ondespawn()
    {
        base.Ondespawn();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stage"))
        {
            //stage.SetStage(other.transform.GetComponent<Stage>());
            //Debug.Log(other.gameObject.name);
            stage = other.transform.GetComponent<Stage>();
        }

        if (other.CompareTag("BridgeBox"))
        {

            onBridge = true;
        }

        if (other.CompareTag("Stair"))
        {



            if (this.colorType == other.gameObject.GetComponent<Stait>().colorType)
            {
                other.gameObject.GetComponent<Stait>().wallStait.SetActive(false);
            }
            else if (colorType != other.gameObject.GetComponent<Stait>().colorType)//!= colorType
            {


                other.gameObject.GetComponent<Stait>().wallStait.SetActive(true);
                if (bricks.Count != 0)//when remove brick
                {
                    //activeBrickWhenRemove = true;
                    int brickcount = bricks.Count;
                    other.gameObject.GetComponent<Stait>().colorType = this.colorType;
                    other.gameObject.GetComponent<Stait>().meshRen.material = meshRen.material;

                    other.gameObject.GetComponent<Stait>().wallStait.SetActive(false);
                    isbridge = false;
                    // other.gameObject.tag = "Untagged";
                    bricks.Remove(bricks[bricks.Count - 1]);
                    brickChild.GetChild(brickChild.childCount - 1).gameObject.SetActive(false);
                    brickChild.GetChild(brickChild.childCount - 1).SetParent(null);

                    stage.SetCharacter(transform.GetComponent<Character>());
                    //Debug.Log(1);

                    //stage.SetCharacter(transform.GetComponent<Character>());

                }

                //else
                //{
                //    //activeBrickWhenRemove = false;
                //    other.gameObject.GetComponent<Stait>().wallStait.SetActive(true);
                //}
            }
            //if(bricks.Count != 0|| this.colorType == other.gameObject.GetComponent<Stait>().colorType)
            //{
            //    //Debug.Log(other.gameObject.GetComponent<Stait>().colorType);
            //    if(this.colorType != other.gameObject.GetComponent<Stait>().colorType)
            //    {
            //        //other.GetComponent<Stait>().meshRen.enabled = true;
            //       other.gameObject.GetComponent<Stait>().colorType = this.colorType;

            //        other.gameObject.GetComponent<MeshRenderer>().material = meshRen.material;
            //        bricks.Remove(bricks[bricks.Count-1]);

            //    }


            //}
            //if(bricks.Count == 0&& this.colorType != other.gameObject.GetComponent<Stait>().colorType)
            //{
            //    other.gameObject.GetComponent<Stait>().wallStait.SetActive(true);
            //    isbridge =false;
            //}

            Vector3 newpos = transform.position;
            //Debug.Log("len");


            newpos.y += 0.15f;

            transform.position = newpos;

            //srb.AddForce(Vector3.up * 50f, ForceMode.Force);

        }
        else if (other.CompareTag("Stair") && _fxJoystick.Vertical < 0)
        {



            Vector3 newpos = transform.position;
            //Debug.Log("len");

            //newpos.y -= 0.15f;

            transform.position = newpos;
        }
        if (other.CompareTag("Brick"))
        {
            if (this.colorType == other.gameObject.GetComponent<Brick>().colorType)
            {
                //Debug.Log(this.meshRen.material);
                GameObject brickPrefab = Instantiate(brick);
                brickPrefab.tag = "Untagged";
                brickPrefab.GetComponent<Brick>().colorType = this.colorType;

                brickPrefab.GetComponent<MeshRenderer>().material = this.meshRen.material;
                bricks.Add(brickPrefab);
                brickPrefab.transform.SetParent(brickChild);
                brickPrefab.transform.localPosition = new Vector3(0, 1 + heightBrick * bricks.Count, -.6f);
                brickPrefab.transform.localRotation = Quaternion.Euler(0, -90, 0);

            }

        }



        //if (other.gameObject.tag == "Brick" && this.colorType == other.gameObject.GetComponent<Brick>().colorType)
        //{
        //    other.gameObject.SetActive(false);
        //    isDiactive = true;
        //    if (isDiactive)
        //    {
        //        Debug.Log("Active");
        //        StartCoroutine(ActiveBrick(other.gameObject));
        //    }
        //}

    }

    //IEnumerator ActiveBrick(GameObject gameobject)
    //{
    //    yield return new WaitForSeconds(timeActive);
    //    //Debug.Log(isDiactive);
    //    //yield return new WaitForSeconds(timeActive);
    //    int colorindex = Random.Range(1, colordata.materials.Length - 1);
    //    gameobject.GetComponent<Brick>().meshRen.material = colordata.materials[colorindex];
    //    colorType = (ColorType)colorindex;
    //    gameobject.SetActive(true);
    //    isDiactive = false;

    //}
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stair"))
        {
            ///other.gameObject.tag = "Stair";
            //Debug.Log("chay xuong deactive wall");
            other.gameObject.GetComponent<Stait>().wallStait.SetActive(false);


        }
        if (other.CompareTag("BridgeBox"))
        {

            onBridge = false;
        }
    }
}
