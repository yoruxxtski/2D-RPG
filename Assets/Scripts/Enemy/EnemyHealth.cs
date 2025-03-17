using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Config")] 
    [SerializeField] private float Health;
    public float CurrentHealth {get; private set;}

    public static event Action OnEnemyDeathEvent;

    private Animator animator;
    private EnemyBrain enemyBrain;
    private EnemySelected enemySelected;
    private Rigidbody2D rb2D;

    private EnemyLoot enemyLoot;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyBrain = GetComponent<EnemyBrain>();
        enemyLoot = GetComponent<EnemyLoot>();
        enemySelected = GetComponent<EnemySelected>();
    } 

    void Start() {
        CurrentHealth = Health;
    }
    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0) {
            DisableEnemy();
            QuestManager.Instance.AddProgress("Kill2Enemy", 1);
            QuestManager.Instance.AddProgress("Kill5Enemy", 1);
            QuestManager.Instance.AddProgress("Kill10Enemy", 1);
        } else {
            DamageManager.Instance.ShowDamageText(amount, transform);
        }
    }

    private void DisableEnemy() {
            animator.SetTrigger("Dead");
            enemyBrain.enabled = false;
            enemySelected.NoSelectedCallBack();
            rb2D.bodyType = RigidbodyType2D.Static;
            OnEnemyDeathEvent?.Invoke();
            GameManager.Instance.AddPlayerExp(enemyLoot.ExpDrop);
    }
}