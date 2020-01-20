using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    CheckPointManager checkPointManager;

    [SerializeField]
    float speed = 6f;            // The speed that the player will move at.
    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    ConstantForce customgravity;        // Gravity bassed on a Vector UP

    [SerializeField]
    Vector3 moveforvard;
    [SerializeField]
    Vector3 moveright;
    float jumpForce = 8.0f;


    public Vector3 forvardRelative;
    public Vector3 rightRelative;

    public bool hasGun = false;

    public Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        customgravity = GetComponent<ConstantForce>();
        GetMovmentDir();
        customgravity.force = -transform.up * 20;       //applying g in a direction of a -normal of a floor

        animator = transform.GetChild(0).Find("Model").GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (hasGun) animator.SetBool("HasGun", true);
        else animator.SetBool("HasGun", false);

        if (h != 0 || v != 0)
            animator.SetBool("IsRunning", true);
        else animator.SetBool("IsRunning", false);
        // Move the player around the scene.
        Move(h, v);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{

        //    playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        //}

        if ((hasGun) && Input.GetMouseButtonDown(0))
            transform.GetChild(0).Find("Gun").gameObject.SetActive(true);

        if ((hasGun) && Input.GetMouseButtonUp(0))
            transform.GetChild(0).Find("Gun").gameObject.SetActive(false);


    }

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement = (h * moveright + v * moveforvard);
        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        Debug.DrawRay(transform.position, moveforvard * 5, Color.blue);
        Debug.DrawRay(transform.position, moveright * 5, Color.red);
        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }


    public void GetMovmentDir()
    {
        moveforvard = Camera.main.transform.forward;
        moveforvard.y = 0;
        moveforvard = moveforvard.normalized;

        forvardRelative = transform.InverseTransformDirection(moveforvard);

        moveright = Camera.main.transform.right;
        moveright.y = 0;
        moveright = moveright.normalized;

        rightRelative = transform.InverseTransformDirection(moveright);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gun")
        {
            hasGun = true;
            Destroy(other.gameObject);
        }

        if (other.tag == "Death" || other.tag == "Ghost")
        {
            //if (checkPointManager.lastactivated == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
