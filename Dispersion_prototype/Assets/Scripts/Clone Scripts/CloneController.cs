using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CloneController : MonoBehaviour
{

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



    void Start()
    {
        
        cloneHealth = GetComponent<CloneHealth>();
        player = GameObject.Find("Player"); //Fix this somehow
        
        cloneRigidbody = GetComponent<Rigidbody>();
        customgravity = GetComponent<ConstantForce>();
        playerController = player.GetComponent<PlayerController>();

        GetMovmentDir();
        customgravity.force = -transform.up * 20;       //applying g in a direction of a -normal of a floor
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

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
        if ((playerController.hasGun) && Input.GetMouseButtonDown(0))
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        if ((playerController.hasGun) && Input.GetMouseButtonUp(0))
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

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

        Debug.DrawRay(transform.position, moveforvard * 5, Color.blue);
        Debug.DrawRay(transform.position, moveright * 5, Color.red);
        // Move the player to it's current position plus the movement.
        cloneRigidbody.MovePosition(transform.position + movement);
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
        if (other.tag == "Death" || other.tag == "Ghost")
        {
           cloneHealth.CloneDeath();
        }
    }

}

