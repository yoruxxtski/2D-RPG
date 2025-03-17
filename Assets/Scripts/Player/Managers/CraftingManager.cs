using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : Singleton<CraftingManager>
{
    [Header("Config")]
    [SerializeField] private RecipeCard recipeCardPrefab;
    [SerializeField] private Transform recipeContainer;
    [SerializeField] private GameObject craftMaterialsPanel;
   
    [Header("Recipes")]
    [SerializeField] private RecipeList recipes;

    [Header("RecipeInfo")]
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Image item1Icon;
    [SerializeField] private TextMeshProUGUI item1Name;
    [SerializeField] private TextMeshProUGUI item1Amount;

    [SerializeField] private Image item2Icon;
    [SerializeField] private TextMeshProUGUI item2Name;
    [SerializeField] private TextMeshProUGUI item2Amount;
    [SerializeField] private Button craftButton;

    [Header("Final Item")]
    [SerializeField] private Image finalItemIcon;
    [SerializeField] private TextMeshProUGUI finalItemName;
    [SerializeField] private TextMeshProUGUI finalItemDescription;

    public Recipe RecipeSelected { get; private set; }




    void Start()
    {
        LoadRecipes();
    }
    private void LoadRecipes() {
        for (int i = 0; i < recipes.recipes.Length; i++) {
            RecipeCard recipeCard = Instantiate(recipeCardPrefab, recipeContainer);
            recipeCard.InitRecipeCard(recipes.recipes[i]);
        }
    }

    public void ShowRecipe(Recipe recipe) {
        if (craftMaterialsPanel.activeSelf == false) {
            craftMaterialsPanel.SetActive(true);
        }
        RecipeSelected = recipe;
        recipeName.text = recipe.Name;
        item1Icon.sprite = recipe.Item1.Icon;
        item1Name.text = recipe.Item1.Name;
        item2Icon.sprite = recipe.Item2.Icon;
        item2Name.text = recipe.Item2.Name;

        item1Amount.text = $"{recipe.Item1Amount}/{Inventory.Instance.GetItemCurrentStock(recipe.Item1.ID)}";
        item2Amount.text = $"{recipe.Item2Amount}/{Inventory.Instance.GetItemCurrentStock(recipe.Item2.ID)}";

        finalItemIcon.sprite = recipe.FinalItem.Icon;
        finalItemName.text = recipe.FinalItem.Name;
        finalItemDescription.text = recipe.FinalItem.Description;

        craftButton.interactable = CalCraftItem(recipe);
    }

    private bool CalCraftItem(Recipe recipe) {
        int item1Stock = Inventory.Instance.GetItemCurrentStock(recipe.Item1.ID);
        int item2Stock = Inventory.Instance.GetItemCurrentStock(recipe.Item2.ID);
        if (item1Stock >= recipe.Item1Amount && item2Stock >= recipe.Item2Amount) return true;
        return false;
    }

    public void CraftItem() {
        for (int i = 0; i < RecipeSelected.Item1Amount; i++) {
            Inventory.Instance.ConsumeItem(RecipeSelected.Item1.ID);
        }

        for (int i = 0; i < RecipeSelected.Item2Amount; i++) {
            Inventory.Instance.ConsumeItem(RecipeSelected.Item2.ID);
        }

        Inventory.Instance.AddItem(RecipeSelected.FinalItem, RecipeSelected.FinalItemAmount);
        ShowRecipe(RecipeSelected);
    }
}