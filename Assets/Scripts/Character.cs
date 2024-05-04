using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected ColorData colordata;

    [SerializeField] private Renderer meshRen;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] private GameObject brick;
    [SerializeField] private Transform brickChild;
    private float heightBrick = 0.42f;
    [SerializeField] private List<GameObject> bricks;
    protected RaycastHit hit;
    [SerializeField] protected LayerMask _layerBrick;
    public Transform _transformBricks;
    public ColorType colorType;
    protected bool isbridge;
    public Stage stage;
    //public bool activeBrickWhenRemove = false;
    [SerializeField] private float timeActive;
    private bool isDiactive;
    public enum animationState
    {
        idle,
        win,
        run,
        fall,
    }
    [SerializeField] private Animator animator;
    private void Start()
    {
        isbridge = true;
    }
    protected virtual void OnInit()
    {

    }
    protected virtual void Ondespawn()
    {

    }
    public void ChangeAnim(animationState eanim)
    {
        animator.SetInteger("State",(int)eanim);
       
    }

    protected void ChangeColor(ColorType ecolor)
    {
        colorType = ecolor;
        meshRen.material = colordata.GetColor(colorType);
    }

    protected virtual void AddBrick()
    {
        
        
            Debug.Log("add brick");
        
    }

    public void RemoveBrick()
    {

    }


    public void ClearBrick() { 


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stair") && rb.velocity.z > 0)
        {

            if(this.colorType == other.gameObject.GetComponent<Stait>().colorType)
            {
                other.gameObject.GetComponent<Stait>().wallStait.SetActive(false);
            }
            else if(this.colorType != other.gameObject.GetComponent<Stait>().colorType)//!= colorType
            {
               
                if (bricks.Count != 0)
                {
                    //activeBrickWhenRemove = true;
                    stage.checkColorPlayerFromStart = true;
                    Debug.Log(other.gameObject.name);
                    
                    other.gameObject.GetComponent<Stait>().colorType = this.colorType;
                    other.gameObject.GetComponent<MeshRenderer>().material = meshRen.material;
                    
                    other.gameObject.GetComponent<Stait>().wallStait.SetActive(false);
                    isbridge = false;
                    other.gameObject.tag = "Untagged";
                    bricks.Remove(bricks[bricks.Count - 1]);
                    brickChild.GetChild(brickChild.childCount-1).gameObject.SetActive(false);
                    brickChild.GetChild(brickChild.childCount - 1).SetParent(null);
                }
                else
                {
                    //activeBrickWhenRemove = false;
                    stage.checkColorPlayerFromStart = true;
                    other.gameObject.GetComponent<Stait>().wallStait.SetActive(true);
                }
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


            //newpos.y += 0.15f;

            transform.position = newpos;

            //srb.AddForce(Vector3.up * 50f, ForceMode.Force);

        }
        else if (other.CompareTag("Stair") && rb.velocity.z < 0)
        {
            //Debug.Log("xuong");
            Vector3 newpos = transform.position;
            //Debug.Log("len");

            //newpos.y -= 0.15f;

            transform.position = newpos;
        }
        if (other.CompareTag("Brick"))
        {
            if(this.colorType == other.gameObject.GetComponent<Brick>().colorType)
            {
                //Debug.Log(this.meshRen.material);
                GameObject brickPrefab = Instantiate(brick);
                brickPrefab.tag = "Untagged";
                brickPrefab.GetComponent<Brick>().colorType = this.colorType;
               
                brickPrefab.GetComponent<MeshRenderer>().material = this.meshRen.material;
                bricks.Add(brickPrefab);
                brickPrefab.transform.SetParent(_transformBricks);
                brickPrefab.transform.localPosition = new Vector3(0, 1 + heightBrick * bricks.Count,-.6f);
                brickPrefab.transform.localRotation = Quaternion.Euler(0,-90,0);
                
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

    IEnumerator ActiveBrick(GameObject gameobject)
    {
        yield return new WaitForSeconds(timeActive);
        //Debug.Log(isDiactive);
        //yield return new WaitForSeconds(timeActive);
        int colorindex = Random.Range(1, colordata.materials.Length - 1);
        gameobject.GetComponent<Brick>().meshRen.material = colordata.materials[colorindex];
        colorType = (ColorType)colorindex;
        gameobject.SetActive(true);
        isDiactive = false;

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stair") && rb.velocity.z > 0)
        {
            other.gameObject.tag = "Stair";

        }
    }
}
