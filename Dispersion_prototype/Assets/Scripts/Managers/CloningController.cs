using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloningController : MonoBehaviour
{
    [SerializeField]
    Transform cloneOriginTransform;
    
    [SerializeField]
    GameObject clonegameobject;

    [SerializeField]
    bool reversemovement = false;
    [SerializeField]
    bool mirrorx = false;
    [SerializeField]
    bool mirrorz = false;

    public bool platformactivated = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && platformactivated == false)
        {
            other.transform.parent = transform.parent;

            Vector3 cloneposition = CalculatePosition(other.transform);
            //Debug.DrawLine(cloneOriginTransform.position, cloneposition, Color.black, 15);

            GameObject newclone = Instantiate(clonegameobject, cloneposition, cloneOriginTransform.rotation, cloneOriginTransform.parent);
            newclone.GetComponent<CloneHealth>().cloningController = this.GetComponent<CloningController>();
            //Chane cloneOriginTransform.rotation to some rotation similar to rotation of a player relative to cloning platform of maybe use local-world-local in clone turning script

            newclone.GetComponent<Inventory>().inventoryItems.AddRange(other.GetComponent<Inventory>().inventoryItems); //adding all items to clone inventory NOT WORKING
            
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
