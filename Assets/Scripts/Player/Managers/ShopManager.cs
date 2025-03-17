using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    [Header("Config")]
    [SerializeField] private ShopCard shopCardPrefab;
    [SerializeField] private Transform shopContainer;

    [Header("Items")]
    [SerializeField] private ShopItem[] items;

    void Start()
    {
        LoadShop();
    }
    private void LoadShop() {
        for (int i = 0; i < items.Length; i++) {
            ShopCard shopCard = Instantiate(shopCardPrefab, shopContainer);
            shopCard.ConfigShopCard(items[i]);
        }
    }
}