using System;
using UnityEngine;

public class EnemyBrain : MonoBehaviour {
    [SerializeField] private string initState;
    [SerializeField] private FSM_State[] states;


    public Transform Player { get; set; }
    public FSM_State CurrentState {get; set;}

    private void Start() {
        ChangeState(initState);
    }


    private void Update() {
        // if (CurrentState == null) return;
        CurrentState?.UpdateState(this);
    }

    public void ChangeState(string newStateID) {
        FSM_State newState = GetState(newStateID);
        if (newState == null) return;
        CurrentState = newState;
    }

    private FSM_State GetState(string newStateID) {
        for (int i = 0; i < states.Length; i++) {
            if (states[i].ID == newStateID) {
                return states[i];
            }
        }
        return null;
    }
}