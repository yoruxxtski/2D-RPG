using System;
using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{   
    [Header("Config")]
    [SerializeField] private GameContent gameContent;
    [SerializeField] private int inventorySize;
    [SerializeField] private InventoryItems[] inventoryItems;
    public InventoryItems[] InventoryItems => inventoryItems;

    [Header("Testing")]
    public InventoryItems testItem;

    public int InventorySize => inventorySize;

    private readonly string INVENTORY_KEY_DATA = "MY_INVENTORY";


    void Start()
    {
        inventoryItems = new InventoryItems[inventorySize];
        // ! ensure init array before verifying else errors
        InventoryUI.Instance.InitInventory();
        VerifyItemsForDraw();
        LoadInventory();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) {
           AddItem(testItem, 1);
        }
    }
    // * Add item
    public void AddItem(InventoryItems item, int quantity) {
        if (item == null || quantity <= 0) return;
        List<int> itemIndexes = CheckItemStockIndexes(item.ID); 
        if (item.isStackable && itemIndexes.Count > 0) {
            foreach (int index in itemIndexes) {
                int maxStack = item.MaxStack;
                if (inventoryItems[index].Quantity < maxStack) {
                    inventoryItems[index].Quantity += quantity;
                    if (inventoryItems[index].Quantity > maxStack) {
                        int difference = inventoryItems[index].Quantity - maxStack;
                        inventoryItems[index].Quantity = maxStack;
                        AddItem(item, difference);
                    }
                    InventoryUI.Instance.DrawItem(inventoryItems[index], index);
                    SaveInventory();
                    return;
                }
            }
        }
        int quantityToAdd = (quantity > item.MaxStack) ? item.MaxStack : quantity;
        AddItemFreeSlot(item, quantityToAdd);
        int remainingAmount = quantity - quantityToAdd;
        if (remainingAmount > 0) {
            AddItem(item, remainingAmount);
        }
        SaveInventory();
    }


    // * Add an item to a free slot 
    private void AddItemFreeSlot(InventoryItems item, int quantity) {
        for (int i = 0; i < inventorySize; i++) {
             if (inventoryItems[i] != null) continue;
             inventoryItems[i] = item.CopyItem();
             inventoryItems[i].Quantity = quantity;
             InventoryUI.Instance.DrawItem(inventoryItems[i], i);
             return;
        }
    }

    // * Use an Item
    private void DecreaseitemStack(int index) {
        inventoryItems[index].Quantity --;
        if (inventoryItems[index].Quantity <= 0) {
            inventoryItems[index] = null;
            InventoryUI.Instance.DrawItem(null, index);
        } else {
            InventoryUI.Instance.DrawItem(inventoryItems[index] , index);
        }
    }

    public void UseItem(int index) {
        if (inventoryItems[index] == null) return;
        if (inventoryItems[index].UseItem()) {
            DecreaseitemStack(index);
        }
        SaveInventory();
    }

    public void RemoveItem(int index) {
        if (inventoryItems[index] == null) return;
        inventoryItems[index].RemoveItem();
        inventoryItems[index] = null;
        InventoryUI.Instance.DrawItem(null, index);
        SaveInventory();
    }

    public void ConsumeItem(string itemID) {
        List<int> indexes = CheckItemStockIndexes(itemID);
        if (indexes.Count > 0) {
            DecreaseitemStack(indexes[^1]);
        }
    }

    public void EquipItem(int index) {
        if (inventoryItems[index] == null) return;
        if (inventoryItems[index].itemType != ItemType.Weapon) return;
        inventoryItems[index].EquipItem();
    }

    // * Save Inventory

    private void SaveInventory() {
        InventoryData saveData = new InventoryData();
        saveData.ItemContent = new string[InventorySize];
        saveData.ItemQuantity = new int[InventorySize];
        for (int i = 0; i < InventorySize; i++) {
            if (inventoryItems[i] == null) {
                saveData.ItemContent[i] = null;
                saveData.ItemQuantity[i] = 0;
            } else {
                saveData.ItemContent[i] = inventoryItems[i].ID;
                saveData.ItemQuantity[i] = inventoryItems[i].Quantity;
            }
        }
        SaveGame.Save(INVENTORY_KEY_DATA, saveData);  
    }

    private InventoryItems ItemExistsInGameContent(string itemID) {
        for (int i = 0; i < gameContent.GameItems.Length; i++) {
            if (gameContent.GameItems[i].ID == itemID)  {
                return gameContent.GameItems[i];
            }
        }
        return null;
    }

    // * Load inventory
    private void LoadInventory() {
        if (SaveGame.Exists(INVENTORY_KEY_DATA)) {
            InventoryData loadData = SaveGame.Load<InventoryData>(INVENTORY_KEY_DATA);
            for (int i = 0; i < inventorySize; i++) {
                if (loadData.ItemContent[i] != null) {
                    InventoryItems itemFromContent = 
                    ItemExistsInGameContent(loadData.ItemContent[i]);
                    if (itemFromContent != null) {
                        inventoryItems[i] = itemFromContent.CopyItem();
                        inventoryItems[i].Quantity = loadData.ItemQuantity[i];
                        InventoryUI.Instance.DrawItem(inventoryItems[i], i);
                    }
                } else {
                    InventoryItems[i] = null;
                }
            }
        }
    }
 
    /*
        * When add a new item , return all indexes of all items in inventory
    */
    private List<int> CheckItemStockIndexes(string itemID) {
        List<int> itemIndexes = new List<int>();
        for (int i = 0; i < inventoryItems.Length; i++) {
            if (inventoryItems[i] == null) continue;
            if (inventoryItems[i].ID == itemID) {
                itemIndexes.Add(i);
            }
        }
        return itemIndexes;
    }

    public int GetItemCurrentStock(string itemID) {
        List<int> indexes = CheckItemStockIndexes(itemID);
        int currentStock = 0;
        foreach (int index in indexes) {
            if (inventoryItems[index].ID == itemID)
            currentStock += inventoryItems[index].Quantity;
        }
        return currentStock;
    }

    

    private void VerifyItemsForDraw()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] == null)
            {
                InventoryUI.Instance.DrawItem(null, i);
            }
        }
    }
}
