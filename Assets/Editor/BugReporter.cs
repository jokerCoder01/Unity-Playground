using UnityEditor;
using UnityEngine;
using System.IO;

public class BugReporter : EditorWindow
{
    #region Private Fields
    GameObject buggyGameObject;
    string bugReportName = "";
    string description = "";
    #endregion

    #region Unity Methods
    void OnGUI()
    {
        GUILayout.BeginVertical("box");

        GUILayout.Space(10);
        GUI.skin.label.fontSize = 24;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("Bug Reporter");
        GUI.skin.label.fontSize = 12;
        GUI.skin.label.alignment = TextAnchor.UpperLeft;

        GUILayout.Space(10);
        bugReportName = EditorGUILayout.TextField("Bug Name", bugReportName);

        GUILayout.Space(10);
        buggyGameObject = (GameObject)EditorGUILayout.ObjectField("Buggy Game Object", buggyGameObject, typeof(GameObject), true);

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Description", GUILayout.MaxWidth(145));
        description = EditorGUILayout.TextArea(description, GUILayout.MaxHeight(75));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("box");
        GUILayout.Space(10);
        GUILayout.Label("Scene : " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().path);
        GUILayout.Label("Time : " + System.DateTime.Now.ToString());
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        GUILayout.BeginHorizontal("box");
        if(GUILayout.Button("Save Bug Report"))
        {
            SaveBugReport();
        }
        if (GUILayout.Button("Save Bug Report + Screenshot"))
        {
            SaveBugReport();
            SaveScreenshot();
        }
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }
    #endregion

    #region Private Methods
    [MenuItem("GYT/Bug Reporter")]
    static void OpenWindow()
    {
        BugReporter bugReporter = (BugReporter)GetWindow(typeof(BugReporter));
        bugReporter.titleContent = new GUIContent("Bug Reporter");
        bugReporter.Show();
    }

    void SaveBugReport()
    {
        Directory.CreateDirectory("Assets/BugReports/" + bugReportName);
        StreamWriter sw = new StreamWriter("Assets/BugReports/" + bugReportName + "/" + bugReportName + ".txt");
        sw.WriteLine(bugReportName);
        sw.WriteLine("Time: " + System.DateTime.Now.ToString());
        sw.WriteLine("Scene: " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().path);
        sw.WriteLine("Buggy Game Object: " + buggyGameObject);
        sw.WriteLine("Description: " + description);
        sw.Close();
    }

    void SaveScreenshot()
    {
        ScreenCapture.CaptureScreenshot("Assets/BugReports/" + bugReportName + "/" + bugReportName + "Screenshot" + ".png");
    }
    #endregion
}