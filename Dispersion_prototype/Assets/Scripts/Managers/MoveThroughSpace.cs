using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveThroughSpace : MonoBehaviour
{
    [SerializeField]
    float speed = 50;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.forward * Time.deltaTime * speed);

        if (transform.position.z <= -4317 && transform.position.z >= -4371)
        {
            Debug.Log("END!");
            GameObject.Find("CheckPoint Manager").GetComponent<CheckPointManager>().lastCheckPoint = 0;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }
    }
}
