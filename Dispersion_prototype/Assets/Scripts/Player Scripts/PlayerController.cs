using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    CheckPointManager checkPointManager;

    PlayerHealth playerHealth;

    [SerializeField]
    float speed = 6f;            // The speed that the player will move at.
    public Vector3 movement;                   // The vector to store the direction of the player's movement.
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

    bool canFloat = false;
    bool floatingFlag = false;
    Vector3 startPositionFloating;

    public int gunDamage = 60; //CHANGE IT TO WEAPON SCRIPT
    public Color gunColor; //CHANGE IT TO WEAPON SCRIPT

    public Animator animator;
    private GameObject turningObject;
    // Start is called before the first frame update
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        customgravity = GetComponent<ConstantForce>();
        playerHealth = GetComponent<PlayerHealth>();
        turningObject = transform.FindChild("PlayerTurning").gameObject;


        customgravity.force = -transform.up * 20;       //applying g in a direction of a -normal of a floor

        animator = transform.GetChild(0).Find("Model").GetComponent<Animator>();
    }

    private void Start()
    {
        checkPointManager = CheckPointManager.instance.GetComponent<CheckPointManager>();
        string lastcheckpointname = string.Format("CheckPoint " + checkPointManager.lastCheckPoint);
        transform.position = GameObject.Find(lastcheckpointname).transform.position;
        hasGun = checkPointManager.hadGun;

        SetMovementRelation();
        GetMovmentDir();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (hasGun) animator.SetBool("HasGun", true);
        else animator.SetBool("HasGun", false);

        if (usingGun) animator.SetBool("UsingGun", true);
        else animator.SetBool("UsingGun", false);

        // Move the player around the scene.
        Move(h, v);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{

        //    playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        //}

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

        if (canFloat && Input.GetKeyDown(KeyCode.Space)) //ADD smooth getting down!
        {
            if (!floatingFlag) startPositionFloating = turningObject.transform.position;
            else turningObject.transform.position = startPositionFloating;

            floatingFlag = !floatingFlag;
        }

    }

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement = (h * moveright + v * moveforvard);
        // Normalise the movement vector and make it proportional to the speed per second.

        movement = movement.normalized * speed * Time.deltaTime;


        Debug.DrawRay(transform.position, moveforvard * 5, Color.blue);
        Debug.DrawRay(transform.position, moveright * 5, Color.red);
        Debug.DrawRay(transform.position, movement * 5, Color.yellow);
        WallStop();

        if (movement.magnitude != 0)
            animator.SetBool("IsRunning", true);
        else animator.SetBool("IsRunning", false);

        if (floatingFlag)
        {
            Floating();
            playerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            playerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }


    private void WallStop()
    {
         RaycastHit hit;

            if (Physics.Raycast(transform.position, movement.normalized, out hit, 1.2f))
            {
                if (hit.transform.tag != "CheckPoint" && hit.transform.tag != "Death" && hit.transform.tag != "Camera Switch" &&
                    hit.transform.tag != "Gravitation" && hit.transform.tag != "Interactable")
                {
                    Debug.DrawRay(transform.position, movement.normalized * 1.2f, Color.green);
                    Debug.Log("Stopped by " + hit.transform.name);
                    movement = Vector3.zero;
                }
            }
    }

    public void GetMovmentDir()
    {
        if (movementRelativeToCam)
        {
            moveforvard = Camera.main.transform.forward;
            moveforvard.y = 0;
            moveforvard = moveforvard.normalized;

            moveright = Camera.main.transform.right;
            moveright.y = 0;
            moveright = moveright.normalized;
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
            GetMovmentDir();
        }
        ///////////////////////////////////////////////////////////////////// PLAYTEST ONLY
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ghost")
        {
            playerHealth.DecreaseHP();
        }

        if (other.tag == "Gravitation")  //Lifting a layer above gravitation plate
        {
            canFloat = true;
        }
    }

    void Floating()
    {
        float floatingHight = Mathf.Sin(Time.time)/1.2f + 1.5f;
        turningObject.transform.position = Vector3.Lerp(turningObject.transform.position, startPositionFloating + new Vector3(0, floatingHight, 0), 0.07f);
    }

    public void SetMovementRelation()
    {
        movementRelativeToCam = OptionsManager.instance.GetComponent<OptionsManager>().movementRelativeToCamOption;
        GetMovmentDir();
    }
}
