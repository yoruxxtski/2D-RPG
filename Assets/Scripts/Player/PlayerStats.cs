using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttributeType {
    Strength, 
    Dexterity,
    Intelligence
}

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Config")]
    public int Level; 

    [Header("Health")]
    public float Health;
    public float MaxHealth;

    [Header("Mana")]
    public float Mana;
    public float MaxMana;

    [Header("Exp")]
    public float CurrentExp;
    public float NextLevelExp;
    public float InitialNextLevelExp;
    [Range(1f, 100f)]public float ExpMultiplier;

    [Header("Attack")]
    public float baseDamage;
    public float criticalChance;
    public float criticalDamage;

    [Header("Attributes")]
    public int Strength;
    public int Dexterity;
    public int Intelligence;
    public int AttributePoints;

    [HideInInspector] public float totalExp;
    [HideInInspector] public float totalDamage;


    public void ResetPlayer() {
        Health = MaxHealth;
        Mana = MaxMana;
        Level = 1;
        CurrentExp = 0;
        NextLevelExp = InitialNextLevelExp;
        totalExp = 0;
        baseDamage = 2;
        criticalChance = 10;
        criticalDamage = 50;
        Strength = 0;
        Dexterity = 0;
        Intelligence = 0;
        AttributePoints = 0;
    }
}
