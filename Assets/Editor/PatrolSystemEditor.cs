/*
 * Gokhan Yahya TORBA
 */

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PatrolSystem))]
public class PatrolSystemEditor : Editor
{

    #region Private Fields
    GameObject patrolManager;
    PatrolSystem ps;
    bool isCreated = false;
    #endregion

    #region Public Methods
    public override void OnInspectorGUI()
    {
        ps = (PatrolSystem)target;
        DrawDefaultInspector();

        DrawCustomEditor();
    }
    #endregion

    #region Private Methods
    void DrawCustomEditor()
    {
        if (GameObject.Find("Patrol System Manager") != null)
        {
            isCreated = true;
        }
        else
        {
            ps.wayPoints.RemoveRange(0, ps.wayPoints.Count);
            if (isCreated == false && GUILayout.Button("Create Patrol Manager"))
            {
                patrolManager = new GameObject("Patrol System Manager");
                isCreated = true;
            }
        }

        if (isCreated)
        {
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Add Waypoint"))
            {
                AddWaypoint();
            }
            if (GUILayout.Button("Delete Waypoint"))
            {
                DeleteWaypoint();
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(30);
            EditorGUILayout.HelpBox("Do not change \"Patrol System Manager\" object name in hierarchy!", MessageType.Warning, true);
        }
    }

    void AddWaypoint()
    {
        GameObject wp = new GameObject(ps.gameObject.name + "Waypoint");
        patrolManager = GameObject.Find("Patrol System Manager");
        wp.transform.SetParent(patrolManager.GetComponent<Transform>());

        ps.wayPoints.Add(wp);
    }

    void DeleteWaypoint()
    {
        DestroyImmediate(ps.wayPoints[ps.wayPoints.Count - 1]);
        ps.wayPoints.RemoveAt(ps.wayPoints.Count - 1);
    }

    ////This code can't work with multiple objects!!!
    //[DrawGizmo(GizmoType.Selected | GizmoType.NonSelected | GizmoType.Active)]
    //static void GizmoDraw(PatrolSystem ps, GizmoType gizmoType)
    //{
    //    for (int i = 0; i < ps.wayPoints.Count; i++)
    //    {
    //        if (i == 0)
    //            Gizmos.color = Color.green;
    //        else if (i == ps.wayPoints.Count - 1)
    //            Gizmos.color = Color.red;
    //        else
    //            Gizmos.color = Color.yellow;

    //        Gizmos.DrawWireCube(ps.wayPoints[i].transform.position, Vector3.one);

    //        if (i != ps.wayPoints.Count - 1)
    //            Gizmos.DrawLine(ps.wayPoints[i].transform.position, ps.wayPoints[i + 1].transform.position);
    //        else
    //        {
    //            if (ps.isLooping)
    //            {
    //                Gizmos.DrawLine(ps.wayPoints[i].transform.position, ps.wayPoints[0].transform.position);
    //            }
    //        }
    //    }
    //}
    #endregion
}