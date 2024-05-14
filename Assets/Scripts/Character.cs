using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected ColorData colordata;

    [SerializeField] protected Renderer meshRen;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected VariableJoystick _fxJoystick;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected GameObject brick;
    [SerializeField] protected Transform brickChild;
    protected float heightBrick = 0.42f;
    [SerializeField] public List<GameObject> bricks;
    protected RaycastHit hit;
    [SerializeField] protected LayerMask _layerBrick;
    //public Transform _transformBricks;
    public ColorType colorType;
    protected bool isbridge;
    public Stage stage;
    //public bool activeBrickWhenRemove = false;
    [SerializeField] private float timeActive;
    //private bool isDiactive;
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
        OnInit();
       
    }

    private void OnApplicationQuit()
    {
        colordata.ResetColorData();
        LevelManager.Instance.ResetTransform();
    }

    protected virtual void OnInit()
    {
        colorType = colordata.randomColor();

        ChangeColor(colorType);


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


    protected void ClearBrick() {

        for (int i = brickChild.childCount-1; i >= 0; i--) {
           GameObject newGame = brickChild.transform.GetChild(i).gameObject;
            newGame.SetActive(false);
            newGame.transform.SetParent(null);
        }
        bricks.Clear();
    }
    public void SetTransform()
    {
        transform.position = LevelManager.Instance.Randomtransform().position;
        OnInit();
        
    }

    private void OnEnable()
    {
        LevelManager.GetTransformEvent += SetTransform;
    }
    private void OnDisable()
    {
        LevelManager.GetTransformEvent -= SetTransform;
    }

}
