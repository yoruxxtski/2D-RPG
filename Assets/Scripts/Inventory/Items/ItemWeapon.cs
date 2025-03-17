using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Weapon", fileName ="ItemWeapon")]
public class ItemWeapon : InventoryItems {
    [Header("Weapon")]
    public Weapons weapons;

    public override void EquipItem()
    {
        WeaponManager.Instance.EquipWeapon(weapons);
    }
}
