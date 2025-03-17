using System;

[Serializable]
public class FSM_State {  
    public string ID;
    public FSM_Action[] Actions; 
    public FSM_Transition[] Transitions;

    public void UpdateState(EnemyBrain enemyBrain) {
        ExecuteActions();
        ExecuteTransitions(enemyBrain);
    }

    private void ExecuteActions() {
        for(int i = 0; i < Actions.Length ; i++) {
            Actions[i].Act();
        }
    } 

    private void ExecuteTransitions(EnemyBrain enemyBrain) {
        if(Transitions == null || Transitions.Length <= 0) {return;}
        for(int i = 0; i < Transitions.Length; i++) {
            bool value = Transitions[i].Decision.Decide();
            if(value) {
                enemyBrain.ChangeState(Transitions[i].TrueState);
            } else {
                enemyBrain.ChangeState(Transitions[i].FalseState);
            }
        }
    }
}