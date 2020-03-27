using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Item
{
    public int weaponDamage;

    public override void Use()
    {
        base.Use();

        GameObject.Find("Player").GetComponent<PlayerController>().hasGun = true;
        //Equip the item
        //remove from the inventory
    }

}
