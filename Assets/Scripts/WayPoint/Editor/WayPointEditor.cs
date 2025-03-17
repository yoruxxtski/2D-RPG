using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// type that this editor can edit -> this editor can edit waypoint
[CustomEditor(typeof(WayPoint))] 
public class WayPointEditor : Editor {
    // WayPointTarget is the target of this editor as WayPoint
    private WayPoint WayPointTarget => target as WayPoint;

    private void OnSceneGUI() {
        if (WayPointTarget.Points.Length <= 0) return;
        Handles.color = Color.red;
        for (int i = 0; i < WayPointTarget.Points.Length; i++) {
            EditorGUI.BeginChangeCheck();
            Vector3 currentPoint = WayPointTarget.EntityPosition + WayPointTarget.Points[i];
            Vector3 newPosition = Handles.FreeMoveHandle(currentPoint, 0.5f, Vector3.one * 0.5f, Handles.SphereHandleCap);

            GUIStyle text = new GUIStyle();
            text.fontStyle = FontStyle.Bold;
            text.fontSize = 16;
            text.normal.textColor = Color.black;
            Vector3 textPos = new Vector3(0.2f , -0.2f);
            Handles.Label(WayPointTarget.EntityPosition 
                         + WayPointTarget.Points[i] + textPos, $"{i + 1}", text);
            
            
            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(target, "Free Move");
                WayPointTarget.Points[i] = newPosition - WayPointTarget.EntityPosition;
            }
        }
    }
}
