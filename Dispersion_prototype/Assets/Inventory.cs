using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 5;

    public List<Item> originalItems = new List<Item>();
    public List<Item> cloneItems = new List<Item>();

    void Awake()
    {
        instance = this;
    }

    public bool Add (Item item)
    {
        if (!item.isDefaultItem)
        {
            if (originalItems.Count >= space)
            {
                Debug.Log("Not enough space");
                return false;
            }

            originalItems.Add(item);
            cloneItems.Add(item);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(Item item)
    {
        originalItems.Remove(item);
        cloneItems.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
