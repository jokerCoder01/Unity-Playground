/*
 * 
 */

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Door))]
[CanEditMultipleObjects]
public class Door_Editor : Editor {

    Door myTarget;

    public static float gizmoSize;
    public static Color gizmoColor;
    public static bool _visualizeHinge;
    public static Vector3 _hingePosition;

    public int toolbarIndex = 0;
    public string[] toolbarStrings = new string[] {"Door", "Rotation", "Interaction", "Sound"};

    public override void OnInspectorGUI()
    {
        myTarget = (Door)target;

        gizmoSize = 0.1f;
        gizmoColor = Color.red;

        toolbarIndex = GUILayout.Toolbar(toolbarIndex, toolbarStrings);
        switch (toolbarIndex)
        {
            case 0:
                DrawDoorSettings();
                break;
            case 1:
                DrawRotationSettings();
                break;
            case 2:
                DrawInteractionSettings();
                break;
            case 3:
                DrawSoundSettings();
                break;
        }
        GUILayout.Label("Version 1.0.0", EditorStyles.centeredGreyMiniLabel);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(myTarget);
            EditorSceneManager.MarkSceneDirty(myTarget.gameObject.scene);
        }
    }

    private void DrawDoorSettings()
    {
        _visualizeHinge = myTarget.visualizeHinge;
        _hingePosition = myTarget.hingePosition;

        GUILayout.BeginVertical();

        GUILayout.Label("Door Settings", EditorStyles.boldLabel);
        myTarget.hingePositionEnum = (Door.HingePosition) EditorGUILayout.EnumPopup("Hinge Position", myTarget.hingePositionEnum);
        FindHingePosition();
        GUILayout.Label("Hinge Position : " + myTarget.hingePosition);
        GUILayout.FlexibleSpace();
        GUILayout.Label("Debug Visualization", EditorStyles.boldLabel);
        myTarget.visualizeHinge = EditorGUILayout.Toggle("Visualize Hinge", myTarget.visualizeHinge);
        if (myTarget.visualizeHinge)
        {
            gizmoSize = EditorGUILayout.FloatField("Gizmo Size", gizmoSize);
            gizmoColor = EditorGUILayout.ColorField("Gizmo Color", gizmoColor);
        }

        GUILayout.EndVertical();
    }

    //TODO
    private void DrawInteractionSettings()
    {
        myTarget.isInteractionActive = EditorGUILayout.Toggle("Interaction Usage", myTarget.isInteractionActive);
        if (myTarget.isInteractionActive)
        {
            GUILayout.BeginVertical();

            myTarget.interactionType = (Door.InteractionType)EditorGUILayout.EnumPopup("Interaction Type", myTarget.interactionType);
            switch (myTarget.interactionType)
            {
                case Door.InteractionType.Click:
                    myTarget.interactionIcon = (Sprite)EditorGUILayout.ObjectField("Interaction Icon", myTarget.interactionIcon, typeof(Sprite), true);
                    //TODO
                    break;
                case Door.InteractionType.Raycast:
                    GUILayout.Label("Placeholder!!!", EditorStyles.boldLabel);
                    //TODO
                    break;
                case Door.InteractionType.Trigger:
                    GUILayout.Label("Placeholder!!!", EditorStyles.boldLabel);
                    //TODO
                    break;
            }

            GUILayout.EndVertical();
        }
    }

    private void DrawRotationSettings()
    {
        GUILayout.BeginVertical();

        myTarget.initialAngle = EditorGUILayout.FloatField("Initial Angle", myTarget.initialAngle);
        myTarget.rotationAngle = EditorGUILayout.FloatField("Rotation Angle", myTarget.rotationAngle);
        myTarget.rotationDirection = (Door.RotationDirection) EditorGUILayout.EnumPopup("Rotation Direction", myTarget.rotationDirection);
        myTarget.speedType = (Door.SpeedType)EditorGUILayout.EnumPopup("Speed Type", myTarget.speedType);

        GUILayout.BeginVertical(myTarget.speedType.ToString() + " Type", "window");
        switch (myTarget.speedType)
        {
            case Door.SpeedType.Float:
                myTarget.rotationSpeed = EditorGUILayout.FloatField("Rotation Speed", myTarget.rotationSpeed);
                break;
            case Door.SpeedType.Time:
                myTarget.rotationTime = EditorGUILayout.FloatField("Rotation Time", myTarget.rotationTime);
                break;
            case Door.SpeedType.Curve:
                myTarget.curve = EditorGUILayout.CurveField("Curve", myTarget.curve, GUILayout.Height(100));
                break;
        }
        GUILayout.EndVertical();

        GUILayout.EndVertical();
    }

    private void DrawSoundSettings()
    {
        myTarget.isSoundActive = EditorGUILayout.Toggle("Sound Usage", myTarget.isSoundActive);
        if (myTarget.isSoundActive)
        {
            GUILayout.BeginVertical("Opening Sounds", "window");
            for (int i = 0; i < myTarget.openingSounds.Count; i++)
            {
                myTarget.openingSounds[i] = (AudioClip)EditorGUILayout.ObjectField("O_Sound_" + i, myTarget.openingSounds[i], typeof(AudioClip), true);
            }
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("+"))
            {
                myTarget.openingSounds.Add(null);
            }
            if (GUILayout.Button("-") && myTarget.openingSounds.Count != 0)
            {
                myTarget.openingSounds.RemoveAt(myTarget.openingSounds.Count - 1);
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            GUILayout.BeginVertical("Closing Sounds", "window");
            for (int i = 0; i < myTarget.closingSounds.Count; i++)
            {
                myTarget.closingSounds[i] = (AudioClip)EditorGUILayout.ObjectField("C_Sound_" + i, myTarget.closingSounds[i], typeof(AudioClip), true);
            }
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("+"))
            {
                myTarget.closingSounds.Add(null);
            }
            if (GUILayout.Button("-") && myTarget.closingSounds.Count != 0)
            {
                myTarget.closingSounds.RemoveAt(myTarget.closingSounds.Count - 1);
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            GUILayout.BeginVertical("Knock Sounds", "window");
            for (int i = 0; i < myTarget.knockSounds.Count; i++)
            {
                myTarget.knockSounds[i] = (AudioClip)EditorGUILayout.ObjectField("K_Sound_" + i, myTarget.knockSounds[i], typeof(AudioClip), true);
            }
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("+"))
            {
                myTarget.knockSounds.Add(null);
            }
            if (GUILayout.Button("-") && myTarget.knockSounds.Count != 0)
            {
                myTarget.knockSounds.RemoveAt(myTarget.knockSounds.Count - 1);
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
        
    }

    private void FindHingePosition()
    {
        myTarget.hingePosition = myTarget.GetComponent<Renderer>().bounds.center;
        switch (myTarget.hingePositionEnum)
        {
            case Door.HingePosition.Top:
                myTarget.hingePosition += new Vector3(0, myTarget.GetComponent<Renderer>().bounds.size.y / 2, 0);
                break;
            case Door.HingePosition.Left:
                myTarget.hingePosition -= new Vector3(myTarget.GetComponent<Renderer>().bounds.size.x / 2, 0, 0);
                break;
            case Door.HingePosition.Bottom:
                myTarget.hingePosition -= new Vector3(0, myTarget.GetComponent<Renderer>().bounds.size.y / 2, 0);
                break;
            case Door.HingePosition.Right:
                myTarget.hingePosition += new Vector3(myTarget.GetComponent<Renderer>().bounds.size.x / 2, 0, 0);
                break;
        }
    }

    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    static void DrawGizmo(Door scr, GizmoType gizmoType)
    {
        if (_visualizeHinge)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(_hingePosition, gizmoSize);
        }
    }
}