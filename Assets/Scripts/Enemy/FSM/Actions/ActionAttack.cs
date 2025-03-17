using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAttack : FSM_Action
{
    [Header("Config")]
    [SerializeField] private float damage;
    [SerializeField] private float timeBtwAttack;
    private float timer;
    
    private EnemyBrain enemyBrain;
    private void Awake() {
        enemyBrain = GetComponent<EnemyBrain>();
    }

    public override void Act()
    {
        AttackPlayer();
    }

    private void AttackPlayer() {
        if (enemyBrain.Player == null) return;
        timer -= Time.deltaTime;
        if (timer <= 0) {
            IDamageable Player = enemyBrain.Player.GetComponent<IDamageable>();
            Player.TakeDamage(damage);
            timer = timeBtwAttack;
        }
    }
}
