using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public static event Action<int> OnSlotSelectedEvent;

    [Header("Config")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image quantityImage;
    [SerializeField] private TextMeshProUGUI itemQuantityTMP;

    public int Index { get; set; }

    public void ClickSlot() {
        OnSlotSelectedEvent?.Invoke(Index);
    }
    public void UpdateSlot(InventoryItems item) {
        itemIcon.sprite = item.Icon;
        itemQuantityTMP.text = item.Quantity.ToString();
    }

    public void ShowSlotInformation(bool value) {
        itemIcon.gameObject.SetActive(value);
        quantityImage.gameObject.SetActive(value);
    }
}