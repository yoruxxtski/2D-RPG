using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static event Action<EnemyBrain> OnEnemySelectedEvent;
    public static event Action OnNoSelectedEvent;

    [Header("Config")]
    [SerializeField] private LayerMask enemyMask;
    private Camera mainCamera;

    void Awake()
    {
        // * Camera.main = access main camera in the current scene
        mainCamera = Camera.main;
    }

    private void SelectEnemy() {
        // * GetMouseButtonDown(0) = left mouse , (1) = right mouse
        if (Input.GetMouseButtonDown(0)) {
            
            RaycastHit2D hit = Physics2D.Raycast(
                mainCamera.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero,
                Mathf.Infinity,
                enemyMask
            );

            if (hit.collider != null) {
                EnemyBrain enemyBrain = hit.collider.GetComponent<EnemyBrain>();
                if (enemyBrain == null) return;
                EnemyHealth enemyHealth = enemyBrain.GetComponent<EnemyHealth>();
                if (enemyHealth.CurrentHealth <= 0f) {
                    EnemyLoot enemyLoot = enemyBrain.GetComponent<EnemyLoot>();
                    LootManager.Instance.ShowLoot(enemyLoot);
                } else {
                    OnEnemySelectedEvent?.Invoke(enemyBrain);
                }                
            } else {
                OnNoSelectedEvent?.Invoke();
            }
        }
    }

    void Update()
    {
        SelectEnemy();
    }
}
