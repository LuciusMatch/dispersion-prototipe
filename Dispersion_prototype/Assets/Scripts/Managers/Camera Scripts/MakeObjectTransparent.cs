using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeObjectTransparent : MonoBehaviour
{
    Transform player;

    List<Vector3> raysToClonesPos = new List<Vector3>();

    List<RaycastHit> hits = new List<RaycastHit>();

    void Start()
    {
        player = GameManager.Instance.player.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //make them non-transp
        MakeTransparent(false);

        Vector3 camToPlayer = player.position - transform.position;

        hits = new List<RaycastHit>();
        
        if (GameManager.Instance.clones.Count > 0)
        {
            GetAllClones();

            foreach (Vector3 rayToClone in raysToClonesPos)
            {
                hits.AddRange(new List<RaycastHit>(Physics.RaycastAll(transform.position, rayToClone, rayToClone.magnitude)));
            }
        }

        hits.AddRange(new List<RaycastHit>(Physics.RaycastAll(transform.position, camToPlayer, camToPlayer.magnitude)));

        MakeTransparent(true);

        Debug.DrawLine(player.position, transform.position, Color.red); 
    }

    void MakeTransparent(bool transparent)
    {
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform != null && hit.transform.gameObject.GetComponent<WallTransparency>() != null)
            {
                hit.transform.gameObject.GetComponent<WallTransparency>().transparentOn = transparent;
            }
        }
    }
     void GetAllClones()
    {
        raysToClonesPos = new List<Vector3>();
        foreach (GameObject clone in GameManager.Instance.clones)
        {
            raysToClonesPos.Add(clone.transform.position - transform.position);
            Debug.DrawLine(clone.transform.position, transform.position, Color.red);
        }
    }
}
