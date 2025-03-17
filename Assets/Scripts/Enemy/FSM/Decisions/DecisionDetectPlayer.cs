using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionDetectPlayer : FSM_Decision
{
    [Header("Config")]
    [SerializeField] private float range;
    [SerializeField] private LayerMask playerMask;
    private EnemyBrain enemy;

    private void Awake() {
        enemy = GetComponent<EnemyBrain>();
    }

    public override bool Decide()
    {
        return DetectPlayer();
    }

    private bool DetectPlayer() {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, range, playerMask);
        if (playerCollider != null) {
            // take player position for monster to chase them
            enemy.Player = playerCollider.transform;
            return true;
        }
        enemy.Player = null;
        return false;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    
}
