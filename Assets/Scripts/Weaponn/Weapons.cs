using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
    Magic,
    Melee
}

[CreateAssetMenu(fileName = "Weapon_")]
public class Weapons : ScriptableObject
{
    [Header("Config")]
   public Sprite Icon;
   public WeaponType weaponType;
   public float damage;
    [Header("Projectile")]
   public Projectiles projectilesPrefab;

   public float RequiredMana;
}
