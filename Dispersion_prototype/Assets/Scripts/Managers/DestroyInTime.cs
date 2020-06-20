using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInTime : MonoBehaviour
{
    [SerializeField]
    float timeToDie = 2;

    void Start()
    {
        StartCoroutine(DoDeath());
    }

    private IEnumerator DoDeath()
    {
        yield return new WaitForSeconds(timeToDie);
        Destroy(gameObject);
    }
}
