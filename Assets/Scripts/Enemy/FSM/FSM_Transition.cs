using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FSM_Transition
{
    public FSM_Decision Decision;
    public string TrueState;
    public string FalseState;
}
