using UnityEngine;

public class DecisionAttackPlayer : FSM_Decision {
    [Header("Config")]
    [SerializeField] private float attack_range;
    [SerializeField] private LayerMask playerMask;

    private EnemyBrain enemy;

    public override bool Decide()
    {
        return PlayerInAttackRange();
    }

    private bool PlayerInAttackRange() {
        if (enemy.Player == null) return false;
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, attack_range, playerMask);
        if (playerCollider != null) return true;
        return false;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attack_range);
    }

    private void Awake() {
        enemy = GetComponent<EnemyBrain>();
    }

}