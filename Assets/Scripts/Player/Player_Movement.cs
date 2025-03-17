using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;
    private PlayerAnimation playerAnimation;
    private PlayerActions actions;
    private Rigidbody2D rb2D;
    private Vector2 moveDirection;

    public Vector2 MoveDirection => moveDirection;

    private Player player;
    

    private void Awake() {
        actions = new PlayerActions();
        rb2D = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        player = GetComponent<Player>();
    }

    private void Update() {
        ReadMovement();
    }
    
    private void FixedUpdate()
    {   
        Move();
    }

    private void Move() {
        if (player.Stats.Health <= 0) return;
        rb2D.MovePosition(rb2D.position + moveDirection * (speed * Time.deltaTime));
    }

    private void ReadMovement() {
        moveDirection = actions.Movement.Move.ReadValue<Vector2>().normalized;
        if (moveDirection == Vector2.zero) {
            playerAnimation.SetMoveBoolTransition(false);
            return;
        }
        playerAnimation.SetMoveBoolTransition(true);
        playerAnimation.SetMoveAnimation(moveDirection);
    }

    private void OnEnable() {
        actions.Enable();
    }

    private void OnDisable() {
        actions.Disable();
    }
}
