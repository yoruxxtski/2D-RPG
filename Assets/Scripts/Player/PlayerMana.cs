using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.O)) {
            UseMana(1f);
        }
    }

    public float GetCurrentMana() {
        return stats.Mana;
    }

    public void UseMana(float amount) {
        if (stats.Mana >= amount) {
            stats.Mana = Mathf.Max(stats.Mana -= amount, 0f);
        }
    }

    public bool CanRestoreMana() {
        return (stats.Mana > 0f && stats.Mana < stats.MaxMana);
    }
    public void RestoreMana(float amount) {
            stats.Mana += amount;
            if (stats.Mana > stats.MaxMana) {
                stats.Mana = stats.MaxMana;
            }
    }
}
