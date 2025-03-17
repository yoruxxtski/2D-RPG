using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Weapon, 
    Potion,
    Scroll,
    Ingredients,
    Treasure
}

[CreateAssetMenu(menuName = "Items/Item")]
public class InventoryItems : ScriptableObject {
    [Header("Config")]
    public string ID;
    public string Name;
    public Sprite Icon;
    [TextArea] public string Description;

    [Header("Info")]
    public ItemType itemType;
    public bool IsConsumable;
    public bool isStackable;
    public int MaxStack;
    [HideInInspector] public int Quantity;
    public InventoryItems CopyItem() {
        InventoryItems instance = Instantiate(this);
        return instance;
    }

    public virtual bool UseItem() {
        return true;
    }
    public virtual void EquipItem() {

    }
    public virtual void RemoveItem() {

    }
}
