/*
 * Gokhan Yahya TORBA
 */
 
using UnityEngine;
using UnityEditor;

public class GridGenerator : EditorWindow {

    #region Private Fields
    private GameObject gridNodePrefab;
    private int rowSize;
    private int columnSize;
    private float padding;
    #endregion

	#region Unity Methods
    [MenuItem("GYT/GridGenerator")]
	static void OpenWindow()
    {
        GridGenerator gridGenerator = (GridGenerator)GetWindow(typeof(GridGenerator));
        gridGenerator.minSize = new Vector2(480, 240);
        gridGenerator.Show();
    } 

    void OnGUI()
    {
        gridNodePrefab = (GameObject)EditorGUILayout.ObjectField("Grid Node", gridNodePrefab, typeof(GameObject), false);
        rowSize = EditorGUILayout.IntField("Row Size", rowSize);
        columnSize = EditorGUILayout.IntField("Column Size", columnSize);
        padding = EditorGUILayout.FloatField("Padding", padding);

        if(GUILayout.Button("Generate Grid"))
        {
            GenerateGrid();
        }
    }
    #endregion

    #region Private Methods
    void GenerateGrid()
    {
        GameObject parent = new GameObject("Grid");

        for(int i = 0; i < rowSize; i++)
        {
            for(int j = 0; j < columnSize; j++)
            {
                GameObject gridNode = Instantiate(gridNodePrefab, new Vector3(i * padding, 0, j * padding), Quaternion.identity);
                gridNode.GetComponent<Transform>().SetParent(parent.GetComponent<Transform>());
            }
        }
    }
    #endregion

}