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
    bool connectionBreak;
    bool connectionFlag;

    public Animator animator;

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
        }

        if ((playerController.movementRelativeToCam) && (mirrormovementX))   //FOR PLAYTEST
        {
            reversemovement = true;
            mirrormovementX = false;
        }


        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (playerController.hasGun) animator.SetBool("HasGun", true);
        else animator.SetBool("HasGun", false);



        if (mirrormovementX == true)
        {
            h *= -1;
        }

        if (mirrormovementZ == true)
        {
            v *= -1;
        }

        if (holdposition == false)
        {
            if (reversemovement == true)
                CopyMove(v, h);
            else
                CopyMove(h, v);
        }

    }
    private void Update()
    {
        GetMovmentDir(); // DUMB!
        cloneIndicator.enabled = true; //Really bad!

        if ((playerController.hasGun) && Input.GetMouseButtonDown(0))

        {
            transform.GetChild(0).Find("Gun").gameObject.SetActive(true);
            transform.GetChild(0).Find("Gun").GetComponent<Renderer>().material.SetColor("_BaseColor", playerController.gunColor);
        }
        if ((playerController.hasGun) && Input.GetMouseButtonUp(0))
            transform.GetChild(0).Find("Gun").gameObject.SetActive(false);

        //if (Input.GetKeyDown(KeyCode.Space) && holdposition == false)
        //{

        //    cloneRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        //}

        if (Input.GetKey(KeyCode.Mouse1))
        {
            holdposition = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            holdposition = false;
        }

        if (holdposition)
        {
            cloneHealth.DecreaseHP();
        }
    }

    void CopyMove(float h, float v)
    {

        // Set the movement vector based on the axis input.
        movement = (h * moveright + v * moveforvard);
        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        movement = movement.normalized * playerController.movement.magnitude;  //Here is the problem! 

        Debug.DrawRay(transform.position, moveforvard * 5, Color.blue);
        Debug.DrawRay(transform.position, moveright * 5, Color.red);
        // Move the player to it's current position plus the movement.

        WallHurt();

        if (connectionBreak)
        {
            WaitForReturn();
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
            if (hit.transform.tag != "CheckPoint" && hit.transform.tag != "Death" && hit.transform.tag != "Camera Switch" &&
                hit.transform.tag != "Gravitation" && hit.transform.tag != "Interactable" && hit.transform.tag != "EnemyTrigger")
            {
                connectionBreak = true;
                if (connectionBreak != connectionFlag)
                {
                    playerPosition = player.transform.position;

                    cloneGoalPosition = transform.position - playerPosition;

                    spherePlayerPosition = GameObject.CreatePrimitive(PrimitiveType.Sphere); //Where player broke the connection
                    //sphereCloneGoalPosition = GameObject.CreatePrimitive(PrimitiveType.Sphere); //Where clone should be 

                    Destroy(spherePlayerPosition.GetComponent<Collider>());
                    //Destroy(sphereCloneGoalPosition.GetComponent<Collider>());


                    connectionFlag = connectionBreak;
                }
           
            }
        }
        
    }

    void GetMovmentDir() // relative movement
    {

        moveforvard = transform.TransformDirection(playerController.forvardRelative);
        moveforvard = moveforvard.normalized;

        moveright = transform.TransformDirection(playerController.rightRelative);
        moveright = moveright.normalized;
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
                   hit.transform.tag != "Gravitation" && hit.transform.tag != "Interactable" && hit.transform.tag != "EnemyTrigger")
            {
                return;
            }
        }
        else transform.position = Vector3.Lerp(transform.position, (player.transform.position + cloneGoalPosition), 0.2f);

        if (vectorToCheck.magnitude < 0.5)
        {
            cloneRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
           
            connectionBreak = false;
        }
    }
}

