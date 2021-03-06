﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloningController : MonoBehaviour
{
    [SerializeField]
    Transform cloneOriginTransform;
    
    [SerializeField]
    GameObject clonegameobject;
    [SerializeField]
    GameObject cloneEffect;

    [SerializeField]
    bool reversemovement = false;
    [SerializeField]
    bool mirrorx = false;
    [SerializeField]
    bool mirrorz = false;

    private bool movingToCenter;

    public bool platformactivated = false;

    private void Update()
    {
        if (movingToCenter)
        {
            GameManager.Instance.player.GetComponent<PlayerController>().movement = Vector3.zero;
            //GameManager.Instance.player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            Vector3 moveToVector = new Vector3(transform.position.x, GameManager.Instance.player.transform.position.y, transform.position.z);

            GameManager.Instance.player.transform.position = 
                Vector3.MoveTowards(GameManager.Instance.player.transform.position, moveToVector, 0.2f);

            if (GameManager.Instance.player.transform.position == moveToVector)
            {
                movingToCenter = false;
                //GameManager.Instance.player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                //GameManager.Instance.player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && platformactivated == false)
        {
            GameManager.audioPlayer.Cloning();

            movingToCenter = true;
            other.transform.parent = transform.parent;
            
            //Vector3 cloneposition = CalculatePosition(other.transform);

            Vector3 cloneposition = cloneOriginTransform.position + cloneOriginTransform.up*1.2f;
            //Debug.DrawLine(cloneOriginTransform.position, cloneposition, Color.black, 15);

            GameObject newclone = Instantiate(clonegameobject, cloneposition, cloneOriginTransform.rotation, cloneOriginTransform.parent);
            Instantiate(cloneEffect, cloneposition, cloneOriginTransform.rotation, cloneOriginTransform.parent); //Effect
        
            GameManager.Instance.clones.Add(newclone);//add a clone to list in manager

            newclone.GetComponent<CloneHealth>().cloningController = this.GetComponent<CloningController>();
            //Chane cloneOriginTransform.rotation to some rotation similar to rotation of a player relative to cloning platform of maybe use local-world-local in clone turning script

            //newclone.GetComponent<Inventory>().inventoryItems.AddRange(other.GetComponent<Inventory>().inventoryItems); //adding all items to clone inventory NOT WORKING
            newclone.GetComponent<KeycardInventory>().SetKeycards(new List<Keycard>(GameManager.Instance.player.GetComponent<KeycardInventory>().keycards));

            //GameObject.Find("Canvas_Levels").GetComponent<Inventory_UI>().inventories.Add(this.GetComponent<Inventory>()); //adding inventory to the list of inventories

            if (reversemovement)
                newclone.GetComponent<CloneController>().reversemovement = true;
            if (mirrorx)
                newclone.GetComponent<CloneController>().mirrormovementX = true;
            if (mirrorz)
                newclone.GetComponent<CloneController>().mirrormovementZ = true;

            platformactivated = true;
        }
    }

    private Vector3 CalculatePosition(Transform playertransform)
    {
        Vector3 enteringVector = playertransform.position - transform.position;
        enteringVector = playertransform.InverseTransformDirection(enteringVector);
        enteringVector = cloneOriginTransform.TransformDirection(enteringVector);

        return cloneOriginTransform.position + enteringVector;
    }

}
