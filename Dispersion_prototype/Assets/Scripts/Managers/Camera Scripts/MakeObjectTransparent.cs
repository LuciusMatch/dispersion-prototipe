using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeObjectTransparent : MonoBehaviour
{
    /// <summary>
    /// Rays should be casted for clones too
    /// Make multiple objects transparent
    /// 
    /// </summary>
    Transform player;


    RaycastHit[] hits = new RaycastHit[0];

    void Start()
    {
        player = GameManager.Instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //make them non-transp
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];

                if (hit.transform.gameObject.GetComponent<WallTransparency>() != null)
                {
                    hit.transform.gameObject.GetComponent<WallTransparency>().transparentOn = false;
                }
            }
        }

        Vector3 camToPlayer = player.position - transform.position;
        hits = Physics.RaycastAll(transform.position, camToPlayer, camToPlayer.magnitude);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            if (hit.transform.gameObject.GetComponent<WallTransparency>() != null)
            {
                hit.transform.gameObject.GetComponent<WallTransparency>().transparentOn = true;
            }
        }

        //RaycastHit hit;

        Debug.DrawLine(player.position, transform.position, Color.red); 

        //if (Physics.Linecast(transform.position, player.position, out hit))
        //{

        //    if (hit.transform.gameObject.GetComponent<WallTransparency>() != null)
        //    {
        //        if (hit.transform != transformHit && transformHit != null)
        //        {
        //            transformHit.gameObject.GetComponent<WallTransparency>().transparentOn = false;
        //        }

        //        transformHit = hit.transform;
        //        transformHit.gameObject.GetComponent<WallTransparency>().transparentOn = true;
        //    }
        //    else
        //    {
        //        if (transformHit != null)
        //            transformHit.gameObject.GetComponent<WallTransparency>().transparentOn = false;
        //    }
        //}
    }
}
