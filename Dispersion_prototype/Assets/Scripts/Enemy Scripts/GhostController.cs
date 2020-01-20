using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GhostController : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    [SerializeField]
    Transform player;

    [SerializeField]
    Transform currentgoal;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentgoal = player;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject currentclone = FindClosestClone();

        if (currentclone == null)
            currentgoal = player;
        else
        {
            if (Vector3.Distance(currentclone.transform.position, transform.position) < Vector3.Distance(player.transform.position, transform.position))
            {
                currentgoal = currentclone.transform;
            }

            else
            {
                currentgoal = player;
            }
        }

        agent.SetDestination(currentgoal.position);
    }

    public GameObject FindClosestClone()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Clone");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}

