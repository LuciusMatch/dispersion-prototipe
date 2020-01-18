using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnabler : MonoBehaviour
{
    [SerializeField]
    Transform enemyOriginTransform;

    [SerializeField]
    GameObject enemygameobject;

    public bool enemyenabled = false;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player" || other.tag == "Clone" )&& enemyenabled == false)
        {
            GameObject newenemy = Instantiate(enemygameobject, enemyOriginTransform.position, enemyOriginTransform.rotation, enemyOriginTransform.parent);
            enemyenabled = true;
        }

    }
}
