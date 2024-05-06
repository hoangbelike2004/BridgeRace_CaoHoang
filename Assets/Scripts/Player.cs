using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.FilePathAttribute;

public class Player : Character
{
    [SerializeField] private float maxSlopeAngle;
    private RaycastHit slopehit;
    
    [SerializeField] private GameObject _canvasJoystick;
    private Touch _touch;
    Vector3 savePosJoyStick,direction;
    [SerializeField] private GameObject pictuePlayer;
    [SerializeField] private LayerMask groundLayermask;
    [SerializeField] private LayerMask stairLayermask2;
    public Transform orientation;
    Vector3 movedirection;
    BoxCollider box;
    Vector3 moveDirection;
    float verticalinput, horizontalinput;

    
    // Start is called before the first frame update
    private void Awake()
    {
        _canvasJoystick.SetActive(false);
    }
    void Start()
    {
        savePosJoyStick = _fxJoystick.transform.position;
        box = GetComponent<BoxCollider>();
        colorType = (ColorType)Random.Range(1, colordata.materials.Length - 1);
        ChangeColor(colorType);
        //_transformBricks.SetParent(transform);
      
    }

    // Update is called once per frame
    void Update()
    {
        movedirection = new Vector3(_fxJoystick.Horizontal, 0, _fxJoystick.Vertical);
        //verticalinput = Input.GetAxis("Vertical");
        //horizontalinput = Input.GetAxis("Horizontal");
        //moveDirection = orientation.forward * _fxJoystick.Vertical + orientation.right * _fxJoystick.Horizontal;
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
        }

        switch (_touch.phase)
        {
            case TouchPhase.Began:
                _canvasJoystick.SetActive(true);

                break;
            
            case TouchPhase.Ended:
                _canvasJoystick.SetActive(false);
                break;
        }
        if (_fxJoystick.Horizontal != 0 || _fxJoystick.Vertical != 0)
        {
            
            //Debug.Log("Anim Run");
            ChangeAnim(animationState.run);
            //_transformBricks.rotation = Quaternion.Euler(20, 0, 0);
         
        }
        if (_fxJoystick.Horizontal == 0 && _fxJoystick.Vertical == 0)
        {
            //Debug.Log("animidle");
            ChangeAnim(animationState.idle);
        }

       
            
        
        //rb.useGravity = !OnSlope();
        if (OnSlope())
        {
            //rb.AddForce(GetSlopeMoveDirection()*moveSpeed*20f,ForceMode.Force);
            if(rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
            if (rb.velocity.z < 0 || rb.velocity.y > 0)
            {
                //rb.velocity = rb.velocity.normalized * moveSpeed;
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
            
                //rb.AddForce(GetSlopeMoveDirection() * moveSpeed, ForceMode.Force);
            
        }
        
        Move();


    }
   
    private void Move()
    {
        //Debug.Log("H: " + _fxJoystick.Horizontal);
        //Debug.Log("V: " + _fxJoystick.Vertical);
        //transform.position += new Vector3(moveSpeed * _fxJoystick.Horizontal*Time.deltaTime, transform.position.y, moveSpeed * _fxJoystick.Vertical*Time.deltaTime);
        rb.velocity = new Vector3(moveSpeed*_fxJoystick.Horizontal,rb.velocity.y,moveSpeed*_fxJoystick.Vertical);
        movedirection.Normalize();
        if (_fxJoystick.Horizontal != 0 || _fxJoystick.Vertical != 0)//
        {

            transform.forward = movedirection;

            //Quaternion qt_rotation = Quaternion.LookRotation(movedirection,Vector3.up);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation,qt_rotation,Time.deltaTime*720f);

            //float angle = Vector3.Angle(rb.velocity, Vector3.forward);
            //float direction = rb.velocity.x > 0 ? angle : -angle;

            //transform.rotation = Quaternion.Euler(0, direction, 0);
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;

        }
        else { rb.constraints = RigidbodyConstraints.FreezeRotation; }

        //Vector3 newEulerAngles = transform.localEulerAngles;
        //newEulerAngles.x = 0;

        //transform.localEulerAngles = newEulerAngles;

        //Quaternion RotaDirection = Quaternion.LookRotation(rb.velocity);
        //RotaDirection.x = 0;
        //transform.rotation = RotaDirection;
        //Debug.Log(s)
        


        //rb.useGravity = !OnSlope();


    }
    private bool OnSlope()
    {

        //Debug.DrawLine(transform.position, transform.position + Vector3.down * 3f, Color.red, 10f);
        if(Physics.Raycast(transform.position, Vector3.down, out slopehit,2*0.5f + 0.3f))
        {
            //Debug.Log("Rum");
            float angle = Vector3.Angle(Vector3.up, slopehit.normal);

            return angle < maxSlopeAngle && angle !=0;
           
        }
        return false;
    }
    //private bool CheckGround()
    //{
    //    Ray ray = new Ray(transform.position,Vector3.down);
    //    RaycastHit hit;
    //    if(Physics.Raycast(ray,out hit,1f, groundLayermask))
    //    {
    //        return true;
    //    }
    //    return false;
    //}
    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopehit.normal).normalized;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Stair")&&rb.velocity.z >0)
    //    {
    //        Vector3 newpos = transform.position;
    //        //Debug.Log("len");


    //        newpos.y += 0.25f;

    //        transform.position = newpos;

    //        //srb.AddForce(Vector3.up * 50f, ForceMode.Force);

    //    }
    //    if (other.CompareTag("Stair") && rb.velocity.z < 0)
    //    {
    //        Debug.Log("xuong");
    //        Vector3 newpos = transform.position;
    //        //Debug.Log("len");

    //        newpos.y -= 0.15f;

    //        transform.position = newpos;
    //    }

    //}

    //private bool IsBrick()
    //{

    //    Vector3 boxPos = new Vector3(transform.position.x,transform.position.y+0.3f,transform.position.z);
    //    return Physics.BoxCast(boxPos, new Vector3(1.5f, 0.3f, 2f), Vector3.forward, out hit, Quaternion.identity, .2f, _layerBrick);

    //}
}
