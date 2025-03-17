using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerStats stats;
    private PlayerAnimation playerAnimation;

    private void Awake() {
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update() {
        if (stats.Health <= 0) {
            PlayerDead();
        }
    }

    public void TakeDamage(float amount)
    {
        if (stats.Health <= 0) return;
        stats.Health -= amount;
        DamageManager.Instance.ShowDamageText(amount, transform);
        if (stats.Health <= 0f) {
            stats.Health = 0f;
            PlayerDead();
        }
    }

    public void RestoreHeath(float amount) {
        stats.Health += amount;
        if (stats.Health > stats.MaxHealth) {
            stats.Health = stats.MaxHealth;
        }
    }
    public bool CanRestoreHealth() {
        return stats.Health > 0 && stats.Health < stats.MaxHealth;
    }
    private void PlayerDead() {
        playerAnimation.SetDeadAnimation();
    }

}
