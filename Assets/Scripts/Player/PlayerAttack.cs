using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;
    [SerializeField] private Weapons initialWeapon; 
    [SerializeField] private Transform[] attackPositions;

    [Header("MeleeConfig")]
    [SerializeField] private ParticleSystem SlashFX;
    [SerializeField] private float minDistanceMeleeAttack;

    public Weapons CurrentWeapon { get; set; }

    private PlayerActions actions;
    private Player_Movement player_Movement;
    private PlayerAnimation playerAnimations;
    private EnemyBrain enemyTarget;
    private Coroutine attackCoroutine;

    private PlayerMana playerMana;

    private Transform currentAttackPosition;
    private float currentAttackRotation;

    void Awake()
    {
        actions = new PlayerActions();
        playerAnimations = GetComponent<PlayerAnimation>();
        player_Movement = GetComponent<Player_Movement>();
        playerMana = GetComponent<PlayerMana>();
    }
    void Start() {
        WeaponManager.Instance.EquipWeapon(initialWeapon);
        actions.Attack.ClickAttack.performed += ctx => Attack();
    }
    void Update()
    {
        GetFirePosition();
    }

    private void Attack() {
        if (enemyTarget == null) return;
        if (attackCoroutine != null) {
            StopCoroutine(attackCoroutine);
        }
        attackCoroutine = StartCoroutine(IEAttack());
    }

    private void MeleeAttack() {
        SlashFX.transform.position = currentAttackPosition.position;
        SlashFX.Play();
        float currentDistanceFromEnemy = 
            Vector3.Distance(enemyTarget.transform.position,
            transform.position);
            if (currentDistanceFromEnemy <= minDistanceMeleeAttack) {
                enemyTarget.GetComponent<IDamageable>().TakeDamage(GetAttackDamage());
            }
    }
    private void MagicAttack() {
        Quaternion rotation = Quaternion.Euler(
                new Vector3(0f , 0f, currentAttackRotation)
            );
            if (playerMana.GetCurrentMana() >= CurrentWeapon.RequiredMana) {
                Projectiles projectiles = Instantiate(CurrentWeapon.projectilesPrefab, 
                currentAttackPosition.position, rotation);
                projectiles.direction = Vector3.up;
                projectiles.damage = GetAttackDamage();
                playerMana.UseMana(CurrentWeapon.RequiredMana);
            }
    }

    private IEnumerator IEAttack() {
        if (currentAttackPosition == null) yield break;
        if (CurrentWeapon.weaponType == WeaponType.Magic) {
            MagicAttack();
        } else {
            MeleeAttack();
        }
        playerAnimations.SetAttackingAnimation(true);
        yield return new WaitForSeconds(0.5f);
        playerAnimations.SetAttackingAnimation(false);
    }

    public void EquipWeapon(Weapons newWeapon) {
        CurrentWeapon = newWeapon;
        stats.totalDamage = stats.baseDamage + CurrentWeapon.damage;
    }

    private float GetAttackDamage() {
        float damage = stats.baseDamage;
        damage += CurrentWeapon.damage;
        float randomPerce = UnityEngine.Random.Range(0f, 100f);
        if (stats.criticalChance >= randomPerce) {
            damage += (int) (damage * (stats.criticalDamage / 100f));
        }
        return damage;
    }

    private void GetFirePosition() {
        Vector2 moveDirection = player_Movement.MoveDirection;
        switch (moveDirection.x) {
            case > 0f: 
                currentAttackPosition = attackPositions[1];
                currentAttackRotation = -90f;
                break;
            case < 0f:
                currentAttackPosition = attackPositions[0];
                currentAttackRotation = -270f;
                break;
        }

        switch (moveDirection.y) {
            case > 0f: 
                currentAttackPosition = attackPositions[2];
                currentAttackRotation = 0;
                break;
            case < 0f:
                currentAttackPosition = attackPositions[3];
                currentAttackRotation = -180f;
                break;
        }
    }

    void OnEnable()
    {
        actions.Enable();
        SelectionManager.OnEnemySelectedEvent += EnemySelectedCallBack;
        SelectionManager.OnNoSelectedEvent += NoSelectedCallBack;
        EnemyHealth.OnEnemyDeathEvent += NoSelectedCallBack;
    }

    private void NoSelectedCallBack()
    {
        enemyTarget = null;
    }

    private void EnemySelectedCallBack(EnemyBrain enemySelected)
    {
        enemyTarget = enemySelected;
    }

    void OnDisable()
    {
        actions.Disable();
        SelectionManager.OnEnemySelectedEvent -= EnemySelectedCallBack;
        SelectionManager.OnNoSelectedEvent -= NoSelectedCallBack;
        EnemyHealth.OnEnemyDeathEvent -= NoSelectedCallBack;
    }
}