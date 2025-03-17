using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float expDrop;
    [SerializeField] private DropItem[] dropItems;
    public List<DropItem> Items { get; private set; }

    public float ExpDrop => expDrop;

    private void Start() {
        LoadDropItems();
    }
    private void LoadDropItems() {
        Items = new List<DropItem>();
        foreach(DropItem item in dropItems) {
            float prob = UnityEngine.Random.Range(0f, 100f);
            if (prob <= item.DropChance) {
                Items.Add(item);
            }
        }
    }
}

[Serializable]
public class DropItem {
    [Header("Config")]
    //* for inspector
    public string Name;
    public InventoryItems Item;
    public int Quantity;

    [Header("Drop Chance")]
    public float DropChance;
    public bool PickedItem {get; set;}
}
