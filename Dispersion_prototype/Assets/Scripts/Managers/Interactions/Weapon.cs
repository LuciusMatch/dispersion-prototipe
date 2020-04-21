using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Item
{
    public int weaponDamage;
    public Color weaponColor;  //ONLY FOR TEST
    //public override void Use()
    //{
    //    base.Use();

    //    GameObject.Find("Player").GetComponent<PlayerController>().hasGun = true;
    //    GameObject.Find("Player").GetComponent<PlayerController>().gunDamage = weaponDamage;
    //    GameObject.Find("Player").GetComponent<PlayerController>().gunColor = weaponColor;
    //    GameObject.Find("Canvas_Levels").GetComponent<Inventory_UI>().SwitchInventoryUI();
    //    //Equip the item
    //    //remove from the inventory
    //}

}
