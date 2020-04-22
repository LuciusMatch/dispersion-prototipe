using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnItemChanged(Inventory inventory);
    public OnItemChanged onItemChangedCallback;

    public int space = 5;

    public List<Item> inventoryItems;
    public GameObject inventoryUI; //for UI


    public void Awake()
    {
        inventoryItems = new List<Item>();
    }

    public bool Add (Item item)
    {
        if (!item.isDefaultItem)
        {
            if (inventoryItems.Count >= space)
            {
                Debug.Log("Not enough space");
                return false;
            }

            inventoryItems.Add(item);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke(this);
        }
        return true;
    }

    public void Remove(Item item)
    {
        inventoryItems.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke(this);
    }
}
