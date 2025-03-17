using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Recipe 
{
    public string Name;
    [Header("Item 1")]
    public InventoryItems Item1;
    public int Item1Amount;

    [Header("Item 2")]
    public InventoryItems Item2;
    public int Item2Amount;

    [Header("Final Item")]
    public InventoryItems FinalItem;
    public int FinalItemAmount;

}
