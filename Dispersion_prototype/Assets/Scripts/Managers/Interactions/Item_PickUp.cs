using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class Item_PickUp : Interactable
{
    public Item item;
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    void PickUp()
    {
        GameObject.Find("Canvas_Levels").GetComponent<Inventory_UI>().SwitchInventoryUI();
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp)
        {
            GameObject.Find("Help_text").GetComponent<Text>().text = ""; //FOR PLAYTESTING
            Destroy(gameObject);
        }
    }
}
