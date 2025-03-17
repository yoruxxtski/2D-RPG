using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : Singleton<InventoryUI>
{
    [Header("Config")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private Transform container;

    [Header("Description Panel")]
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private Image ItemIcon;
    [SerializeField] private TextMeshProUGUI itemNameTMP;
    [SerializeField] private TextMeshProUGUI itemDescriptionTMP;

    

    public InventorySlot CurrentSlot { get; set; }
    private List<InventorySlot> slotList = new List<InventorySlot>();

    public void InitInventory() {
        for (int i = 0 ; i < Inventory.Instance.InventorySize; i++) {
            InventorySlot inventorySlot = Instantiate(slotPrefab, container);
            inventorySlot.Index = i;
            slotList.Add(inventorySlot);
        }
    }

    public void UseItem() {
        if (CurrentSlot == null) return;
        Inventory.Instance.UseItem(CurrentSlot.Index);
    }
    public void RemoveItem() {
        if (CurrentSlot == null) return;
        Inventory.Instance.RemoveItem(CurrentSlot.Index);
    }

    public void EquipItem() {
        if (CurrentSlot == null) return;
        Inventory.Instance.EquipItem(CurrentSlot.Index);
    }

    public void DrawItem(InventoryItems item, int index) {
        InventorySlot slot = slotList[index];
        if (item == null) {
            slot.ShowSlotInformation(false);
            return;
        }
        slot.ShowSlotInformation(true);
        slot.UpdateSlot(item);
    }

    public void ShowItemDescription(int index) {
        if (Inventory.Instance.InventoryItems[index] == null) return;
        descriptionPanel.SetActive(true);
        ItemIcon.sprite = Inventory.Instance.InventoryItems[index].Icon;
        itemNameTMP.text = Inventory.Instance.InventoryItems[index].Name;
        itemDescriptionTMP.text = Inventory.Instance.InventoryItems[index].Description;
    }

    public void OpenCloseInventory() {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (inventoryPanel.activeSelf == false) {
            descriptionPanel.SetActive(false);
            CurrentSlot = null;
        }
    }

    private void SlotSelectedCallback(int slotIndex) {
        CurrentSlot = slotList[slotIndex];
        ShowItemDescription(slotIndex);
    }

    void OnEnable()
    {
        InventorySlot.OnSlotSelectedEvent += SlotSelectedCallback;
    }

    void OnDisable()
    {
        InventorySlot.OnSlotSelectedEvent -= SlotSelectedCallback;
        
    }
}