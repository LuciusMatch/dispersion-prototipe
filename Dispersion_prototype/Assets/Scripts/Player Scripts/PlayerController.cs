using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices.ComTypes;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    CheckPointManager checkPointManager;

    PlayerHealth playerHealth;

    [SerializeField]
    float speed = 6f;            // The speed that the player will move at.
    public Vector3 movement;                   // The vector to store the direction of the player's movement.
    public bool stoppedByClone;

    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    ConstantForce customgravity;        // Gravity bassed on a Vector UP

    [SerializeField]
    Vector3 moveforvard;
    [SerializeField]
    Vector3 moveright;
    float jumpForce = 8.0f;


    public bool movementRelativeToCam = true; //forward and right of a character is relative to forward and right of a camera
    public Vector3 forvardRelative;
    public Vector3 rightRelative;

    public bool hasGun = false;
    public bool usingGun = false; //FOR PLAYTEST

    bool floating;

    public int gunDamage = 60; //CHANGE IT TO WEAPON SCRIPT
    public Color gunColor; //CHANGE IT TO WEAPON SCRIPT

    public Animator animator;
    private GameObject turningObject;

    private PlayerControls input;
    public Vector2 moveInput;

    // Start is called before the first frame update
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        customgravity = GetComponent<ConstantForce>();
        playerHealth = GetComponent<PlayerHealth>();
        turningObject = transform.Find("PlayerTurning").gameObject;

        customgravity.force = -transform.up * 50;       //applying g in a direction of a -normal of a floor

        animator = transform.GetChild(0).Find("Model").GetComponent<Animator>();

        input = new PlayerControls();
        input.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
    }

    private void Start()
    {
        checkPointManager = CheckPointManager.instance;
        transform.position = checkPointManager.GetLastCheckPoint().transform.position;
        hasGun = checkPointManager.hadGun;

        SetMovementRelation();
        GetMovmentDir(GameManager.Instance.mainCamera.transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // Store the input axes.
        float h = moveInput.x;
        float v = moveInput.y;

        if (hasGun) animator.SetBool("HasGun", true);
        else animator.SetBool("HasGun", false);

        if (usingGun) animator.SetBool("UsingGun", true);
        else animator.SetBool("UsingGun", false);

        // Move the player around the scene.

        Move(h, v);
    }

    private void Update()
    {


        if (hasGun && Input.GetMouseButtonDown(0))
        {
            transform.GetChild(0).Find("Gun").gameObject.SetActive(true);  //DUMB!! NEED TO CHANGE IT
            transform.GetChild(0).Find("Gun").GetComponent<Renderer>().material.SetColor("_BaseColor", gunColor);
            usingGun = true;
        }

        if (hasGun && Input.GetMouseButtonUp(0))

        {
            transform.GetChild(0).Find("Gun").gameObject.SetActive(false);  //DUMB! NEED TO CHANGE IT
            usingGun = false;
        }

        if (GameManager.Instance.clones.Count > 0)
        {
            AreClonesBlocked();
        }
        else
          stoppedByClone = false; 
    }

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement = (h * moveright + v * moveforvard);
        // Normalise the movement vector and make it proportional to the speed per second.

        RaycastHit hit;
        Physics.SphereCast(transform.position, .25f, Vector3.down, out hit, 3f, LayerMask.GetMask("Floor"));
        float slope = Vector3.Dot(turningObject.transform.right, (Vector3.Cross(Vector3.up, hit.normal)));
        slope = (slope < 0) ? slope * 0.25f : slope * 0.6f;
        //Debug.Log(slope);
        movement = movement.normalized * speed * (1 - slope) * Time.deltaTime;


        Debug.DrawRay(transform.position, moveforvard * 5, Color.blue);
        Debug.DrawRay(transform.position, moveright * 5, Color.red);
        Debug.DrawRay(transform.position, movement * 5, Color.yellow);
        WallStop();

        if (stoppedByClone)
            movement = Vector3.zero;

        if (movement.magnitude != 0)
        animator.SetBool("IsRunning", true);
        else animator.SetBool("IsRunning", false);

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);

        if (floating)
        {
            Floating();
        }
        else
            turningObject.transform.localPosition = Vector3.Lerp(turningObject.transform.localPosition, new Vector3(0, 0, 0), 0.05f);
    }


    private void WallStop()
    {
        RaycastHit hit;

        Vector3 rayStartPoint = transform.position + 0.5f * Vector3.down;
        if (Physics.Raycast(rayStartPoint, movement.normalized, out hit, 1f))
        {
            if (!hit.transform.GetComponent<Collider>().isTrigger)
            {
                if (hit.transform.tag != "CheckPoint" && hit.transform.tag != "Death" && hit.transform.tag != "Camera Switch" &&
                    hit.transform.tag != "Gravitation" && hit.transform.tag != "Interactable" && hit.transform.tag != "EnemyTrigger" &&
                    hit.transform.tag != "Simple Gravitation" && hit.transform.tag != "Stairs")
                {
                    Debug.DrawRay(rayStartPoint, movement.normalized * 1.2f, Color.green);
                    //Debug.Log("Stopped by " + hit.transform.name);
                    movement = Vector3.zero;
                }

                if (hit.transform.tag == "Simple Gravitation")
                {
                    //floating = true;
                    Vector3 gravi = hit.transform.forward;
                    //Debug.Log(gravi + "; " + movement);
                    if (gravi.x != 0)
                    {
                        movement.z = (movement.x * gravi.x >= 0) ? 0 : movement.z;
                    }
                    else
                    {
                        movement.x = (movement.z * gravi.z >= 0) ? 0 : movement.x;
                    }
                }

                if (hit.transform.tag == "Death")
                    playerHealth.Death();
            }

        }
        else
        {
            floating = false;
        }

    }


    public void GetMovmentDir(Transform camPos)
    {
        if (movementRelativeToCam)
        {
            moveforvard = camPos.forward;
            moveforvard.y = 0;
            moveforvard = moveforvard.normalized;

            //moveright = Camera.main.transform.right;
            //moveright.y = 0;
            //moveright = moveright.normalized;
            moveright = -Vector3.Cross(moveforvard, transform.up).normalized;
        }
        else
        {
            moveforvard = transform.forward;
            moveright = transform.right;
        }

        forvardRelative = transform.InverseTransformDirection(moveforvard);
        rightRelative = transform.InverseTransformDirection(moveright);
    }

    private void OnTriggerEnter(Collider other)
    {
       // if (other.tag == "Gun")
     //  {
      //      hasGun = true;
      //      Destroy(other.gameObject);
       // }

        if (other.tag == "Death")
        {
        
            playerHealth.Death();
        }
        ///////////////////////////////////////////////////////////////////// PLAYTEST ONLY
        if (other.name == "CamSwith_4_5(1)")
        {
            movementRelativeToCam = true;
            GetMovmentDir(GameManager.Instance.mainCamera.transform);
        }
        ///////////////////////////////////////////////////////////////////// PLAYTEST ONLY
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ghost")
        {
            playerHealth.DecreaseHP();
        }
    }

    private void OnTriggerExit(Collider other)
    { 
    }

    void Floating()
    {
        float floatingHight = Mathf.Sin(Time.time*5)/1.5f + 1.5f;
        turningObject.transform.localPosition = Vector3.Lerp(turningObject.transform.localPosition, 
            new Vector3(0, floatingHight, 0) - turningObject.transform.TransformDirection(floatingHight * Vector3.forward), 0.05f);
    }

    public void SetMovementRelation()
    {
        movementRelativeToCam = OptionsManager.movementRelativeToCamOption;
        GetMovmentDir(GameManager.Instance.mainCamera.transform);
    }

    public void DeathFreeze()
    {
        speed = 0;
        turningObject.GetComponent<PlayerTurning>().turningspeed = 0;
    }

    void AreClonesBlocked()
    {
        foreach (GameObject clone in GameManager.Instance.clones)
        {
            if (clone.GetComponent<CloneController>().cloneIsBlocked)
            {
                stoppedByClone = true;
            }
            else
            {
                stoppedByClone = false;
            }
        }
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
