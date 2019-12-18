using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GhostController : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    [SerializeField]
    Transform goal;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(goal.position);
    }
}
