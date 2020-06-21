using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    Image cloneIndicator;

    [SerializeField]
    float speed = 6f;            // The speed that the player will move at.

    CloneHealth cloneHealth;

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Rigidbody cloneRigidbody;          // Reference to the player's rigidbody.
    ConstantForce customgravity;        // Gravity bassed on a Vector UP

    [SerializeField]
    Vector3 moveforvard;
    [SerializeField]
    Vector3 moveright;
    float jumpForce = 8.0f;

    bool holdposition;

    GameObject player;
    PlayerController playerController;
    public bool reversemovement = false;
    public bool mirrormovementX = false;
    public bool mirrormovementZ = false;


    Vector3 playerPosition;
    GameObject spherePlayerPosition;
    GameObject sphereCloneGoalPosition;
    Vector3 cloneGoalPosition; //Where the clone shoul be when it's stuck

    public bool cloneIsBlocked;

    bool connectionBreak;
    bool connectionFlag;

    bool relativeMovementFalg;

    public Animator animator;

    private PlayerControls input;

    private void Awake()
    {
        input = new PlayerControls();
    }

    void Start()
    {
        cloneHealth = GetComponent<CloneHealth>();

 
        player = GameObject.Find("Player"); //Fix this somehow
        
        cloneRigidbody = GetComponent<Rigidbody>();
        customgravity = GetComponent<ConstantForce>();
        playerController = player.GetComponent<PlayerController>();

        GetMovmentDir();
        customgravity.force = -transform.up * 20;       //applying g in a direction of a -normal of a floor

        animator = transform.GetChild(0).Find("Model").GetComponent<Animator>();
        cloneIndicator = GameObject.Find("CloneIndicator").GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((!playerController.movementRelativeToCam) && (reversemovement))  //FOR PLAYTEST
        {
            reversemovement = false;
            mirrormovementX = true;
            relativeMovementFalg = true;
        }

        if ((playerController.movementRelativeToCam) && (mirrormovementX) && relativeMovementFalg)   //FOR PLAYTEST
        {
            reversemovement = true;
            mirrormovementX = false;
        }


        float h = playerController.moveInput.x;
        float v = playerController.moveInput.y;

        if (playerController.hasGun) animator.SetBool("HasGun", true);
        else animator.SetBool("HasGun", false);



            //if (mirrormovementX == true)
            //{
            //    h *= -1;
            //}

            //if (mirrormovementZ == true)
            //{
            //    v *= -1;
            //}

        if (holdposition == false)
        {
            if (reversemovement == true)
                CopyMove(v, h);
            else
                CopyMove(h, v);
        }


        //// STOP PLAYER BY CLONE
        //if (cloneIsBlocked)
        //    playerController.stoppedByClone = true;
        //else
        //    playerController.stoppedByClone = false;
        ////
    }
    private void Update()
    {
        GetMovmentDir(); // DUMB!
        cloneIndicator.enabled = true; //Really bad!

        /*if ((playerController.hasGun) && Input.GetMouseButtonDown(0))

        {
            transform.GetChild(0).Find("Gun").gameObject.SetActive(true);
            transform.GetChild(0).Find("Gun").GetComponent<Renderer>().material.SetColor("_BaseColor", playerController.gunColor);
        }
        if ((playerController.hasGun) && Input.GetMouseButtonUp(0))
            transform.GetChild(0).Find("Gun").gameObject.SetActive(false);*/

        //if (Input.GetKeyDown(KeyCode.Space) && holdposition == false)
        //{

        //    cloneRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        //}

        holdposition = input.Gameplay.FreezeClone.triggered;
        
        /*if(Input.GetKey(KeyCode.Mouse1))
        {
            holdposition = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            holdposition = false;
        }*/

        if (holdposition)
        {
            cloneHealth.DecreaseHP();
        }

        if (player.GetComponent<PlayerHealth>().curHealth <= 0)
            animator.SetBool("Death", true);
    }

    void CopyMove(float h, float v)
    {
        
        // Set the movement vector based on the axis input.
        movement = (h * moveright + v * moveforvard);
        Debug.DrawRay(transform.position, movement.normalized * 6, Color.green);
        WallHurt();
        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;
        movement = movement.normalized * playerController.movement.magnitude;

        Debug.DrawRay(transform.position, movement.normalized * 5, Color.green);
        Debug.DrawRay(transform.position, moveforvard * 5, Color.blue);
        Debug.DrawRay(transform.position, moveright * 5, Color.red);
        // Move the player to it's current position plus the movement.
        
        //movement = playerController.movement;
        

        if (connectionBreak)
        {
            //WaitForReturn(); // Not sure about this "return feature"
        }
        else
        {
            connectionFlag = false;
            cloneRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if (movement.magnitude != 0)
            animator.SetBool("IsRunning", true);
        else animator.SetBool("IsRunning", false);

        cloneRigidbody.MovePosition(transform.position + movement);
    }

    private void WallHurt()
    {
        
        RaycastHit hit;

        if (Physics.Raycast(transform.position, movement.normalized, out hit, 1.2f))
        {
            if (!hit.transform.GetComponent<Collider>().isTrigger)
            {
                if (hit.transform.tag != "CheckPoint" && hit.transform.tag != "Death" && hit.transform.tag != "Camera Switch" &&
                hit.transform.tag != "Gravitation" && hit.transform.tag != "Interactable" && hit.transform.tag != "EnemyTrigger")
                {
                    cloneIsBlocked = true;
                    //cloneHealth.DecreaseHP();
                    movement = Vector3.zero;
                    
                    //connectionBreak = true;
                    //if (connectionBreak != connectionFlag) // Not sure about this "return feature"
                    //{
                    //    playerPosition = player.transform.position;

                    //    cloneGoalPosition = transform.position - playerPosition;

                    //    spherePlayerPosition = GameObject.CreatePrimitive(PrimitiveType.Sphere); //Where player broke the connection
                    //    //sphereCloneGoalPosition = GameObject.CreatePrimitive(PrimitiveType.Sphere); //Where clone should be 

                    //    Destroy(spherePlayerPosition.GetComponent<Collider>());
                    //    //Destroy(sphereCloneGoalPosition.GetComponent<Collider>());


                    //    connectionFlag = connectionBreak;
                    //}

                }
                else cloneIsBlocked = false;

                if (hit.transform.tag == "Death")
                    cloneHealth.CloneDeath();

            }
        }
        else cloneIsBlocked = false;


    }

    void GetMovmentDir() // relative movement
    {

        moveforvard = transform.TransformDirection(playerController.forvardRelative);
        moveforvard = moveforvard.normalized;

        //moveright = transform.TransformDirection(playerController.rightRelative);
        //moveright = moveright.normalized;
        moveright = -Vector3.Cross(moveforvard, transform.up).normalized;

        if (mirrormovementZ == true)
        {
            moveforvard *= -1;
        }

        if (mirrormovementX == true)
        {
            moveright *= -1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Death")
        {
            cloneIndicator.enabled = false;
            cloneHealth.CloneDeath();
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ghost")
        {
            cloneHealth.DecreaseHP();
        }
        //curHealth -= Time.deltaTime * damagingSpeed;
    }

    void WaitForReturn()
    {

        Vector3 relativeVector = playerPosition - player.transform.position;

        

        if (Mathf.Abs(relativeVector.magnitude) > 0.5)
        {
            //sphereCloneGoalPosition.transform.position = player.transform.position + cloneGoalPosition;
            spherePlayerPosition.transform.position = playerPosition;
            
            cloneRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            cloneHealth.DecreaseHP();
            returnClone();
        }
        else
        {
            cloneRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            connectionBreak = false; 
        }

    }


    void returnClone()
    {
        RaycastHit hit;
        Vector3 vectorToCheck = transform.position - (player.transform.position + cloneGoalPosition);

        if (Physics.Linecast(transform.position, (player.transform.position + cloneGoalPosition),  out hit))
        {
            if (hit.transform.tag != "CheckPoint" && hit.transform.tag != "Camera Switch" &&
                   hit.transform.tag != "Gravitation" && hit.transform.tag != "Interactable" && hit.transform.tag != "EnemyTrigger" && hit.transform.tag != "Simple Gravitation")
            {
                return;
            }
        }
        else transform.position = Vector3.Lerp(transform.position, (player.transform.position + cloneGoalPosition), 0.05f);

        if (vectorToCheck.magnitude < 0.5)
        {
            cloneRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
           
            connectionBreak = false;
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

