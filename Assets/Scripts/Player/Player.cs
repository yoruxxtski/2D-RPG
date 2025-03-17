using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;
    
    [Header("Test")]
    public ItemHealthPotion HealthPotion;
    public ItemManaPotion ManaPotion;

    private PlayerAnimation animations;
    public PlayerHealth PlayerHealth { get; private set; }
    public PlayerMana PlayerMana { get; private set; }

    public PlayerAttack PlayerAttack { get; private set; }

    private void Awake() {
        animations = GetComponent<PlayerAnimation>();
        PlayerHealth = GetComponent<PlayerHealth>();
        PlayerMana = GetComponent<PlayerMana>();
        PlayerAttack = GetComponent<PlayerAttack>();
    }

    public PlayerStats Stats => stats;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) {
            if (HealthPotion.UseItem()) {
            Debug.Log("Using Health Potion");
            }
        }

        if (Input.GetKeyDown(KeyCode.Y)) {
            if (ManaPotion.UseItem()) {
                Debug.Log("Using Mana Potion");
            }
        }
    }

    public void ResetPlayer() {
        stats.ResetPlayer();
        // Reset Animation
        animations.ResetPlayer();
    }
}
