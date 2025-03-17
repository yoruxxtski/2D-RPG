using UnityEngine;

[CreateAssetMenu(fileName = "ItemManaPotion" , menuName = "Items/Mana Potion" )]
public class ItemManaPotion : InventoryItems
{
    [Header("Config")]
    public float ManaValue;

    public override bool UseItem()
    {
        if (GameManager.Instance.Player.PlayerMana.CanRestoreMana()) {
            GameManager.Instance.Player.PlayerMana.RestoreMana(ManaValue);
            return true;
        }
        return false;
    }
}