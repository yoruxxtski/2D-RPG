using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// represent actions that enemy can perform
public abstract class FSM_Action : MonoBehaviour
{
    public abstract void Act();   
}
