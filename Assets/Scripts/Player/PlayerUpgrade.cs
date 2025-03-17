using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    public static event Action OnPlayerUpgradeEvent;

    [Header("Config")]
    [SerializeField] private PlayerStats stats;
    [Header("Settings")]
    [SerializeField] private UpgradeSettings[] settings;

    private void UpgradePlayer(int upgradeIndex) {
        stats.baseDamage += settings[upgradeIndex].damageUpgrade;
        stats.totalDamage += settings[upgradeIndex].damageUpgrade;
        stats.MaxHealth += settings[upgradeIndex].healthUpgrade;
        stats.Health = stats.MaxHealth;
        stats.MaxMana += settings[upgradeIndex].ManaUpgrade;
        stats.Mana = stats.MaxMana;
        stats.criticalChance += settings[upgradeIndex].CChanceUpgrade;
        stats.criticalDamage += settings[upgradeIndex].CDamageUpgrade;
    }

    private void AttributeCallBack(AttributeType attributeType) {
        if (stats.AttributePoints == 0) return;
        switch(attributeType) {
            case AttributeType.Strength:
                UpgradePlayer(0);
                stats.Strength ++;
                break;
            case AttributeType.Dexterity:
                UpgradePlayer(1);
                stats.Dexterity ++;
                break;
            case AttributeType.Intelligence:
                UpgradePlayer(2);
                stats.Intelligence ++;
                break;
        }
        stats.AttributePoints --;
        OnPlayerUpgradeEvent?.Invoke();
    }

    void OnEnable()
    {
        AttributeButton.OnAttributeSelectedEvent += AttributeCallBack;
    }

    void OnDisable()
    {
        AttributeButton.OnAttributeSelectedEvent -= AttributeCallBack;
        
    }
}

[Serializable]
public class UpgradeSettings {
    public string Name;

    [Header("Values")]
    public float damageUpgrade;
    public float healthUpgrade;
    public float ManaUpgrade;
    public float CChanceUpgrade;
    public float CDamageUpgrade;
}
