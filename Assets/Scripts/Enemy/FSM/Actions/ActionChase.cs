using UnityEngine;
public class ActionChase : FSM_Action {

    [Header("Config")]
    [SerializeField] private float ChaseSpeed;
    private EnemyBrain enemyBrain;

    private void Awake() {
        enemyBrain = GetComponent<EnemyBrain>();        
    }

    public override void Act()
    {
        ChasePlayer();
    }

    private void ChasePlayer() {
        if (enemyBrain.Player == null){
            return;
        } 
        Vector3 dirToPlayer = enemyBrain.Player.position - transform.position;
        if (dirToPlayer.magnitude >= 1.3) {
            transform.Translate(dirToPlayer.normalized * (ChaseSpeed * Time.deltaTime));
        }
    }
}