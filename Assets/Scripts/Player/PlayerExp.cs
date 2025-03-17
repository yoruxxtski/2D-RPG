using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    public static event Action PlayerUpgradeEvent;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.X)) {
            AddExp(300f);
        }
    }

    public void AddExp(float amount) {
        stats.totalExp += amount;
        stats.CurrentExp += amount;
        while (stats.CurrentExp >= stats.NextLevelExp) {
            stats.CurrentExp -= stats.NextLevelExp;
            NextLevel();
        }
    }

    private void NextLevel() {
        stats.Level ++;
        stats.AttributePoints ++;
        float currentExpRequired = stats.NextLevelExp;
        float newNextLevelExp = Mathf.Round(currentExpRequired + stats.NextLevelExp * (stats.ExpMultiplier / 100f));
        stats.NextLevelExp = newNextLevelExp;
        PlayerUpgradeEvent?.Invoke();
    }
}
