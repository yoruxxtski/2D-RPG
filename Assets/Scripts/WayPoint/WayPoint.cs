using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [Header("Config")]
    // Tuong doi so voi entity position
    [SerializeField] private Vector3[] points;

    // prop
    public Vector3 EntityPosition { get; set; }
    
    // 
    public Vector3[] Points => points;

    private bool gameStarted;
    
    private void Start() {
        EntityPosition = transform.position;
        gameStarted = true;
    }

    private void OnDrawGizmos() {
        if (gameStarted == false && transform.hasChanged) {
            EntityPosition = transform.position;
        }
    }

    public Vector3 GetPosition(int pointIndex) {
        return EntityPosition + points[pointIndex];
    }

}
