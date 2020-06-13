using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windController : MonoBehaviour
{
    public bool windIsOn;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            InvokeRepeating("StartWindCoroutine", 1.0f, 6.0f);
        }
        //curHealth -= Time.deltaTime * damagingSpeed;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            WindOff();
        }
        //curHealth -= Time.deltaTime * damagingSpeed;
    }

    void StartWindCoroutine()
    {
        StartCoroutine(WindCoroutine());
    }

    IEnumerator WindCoroutine()
    {
        WindOn();
        windIsOn = true;
        yield return new WaitForSeconds(3);

        WindOff();
        windIsOn = false;
    }

    void WindOn()
    {
        player.GetComponent<ConstantForce>().force -= transform.forward * 20f;
    }

    void WindOff()
    {
        player.GetComponent<ConstantForce>().force += transform.forward * 20f;
    }
}
