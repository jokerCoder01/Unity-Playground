/*
 * Gokhan Yahya TORBA
 */
 
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class PatrolSystem : MonoBehaviour {

    #region Public Fields
    public float movementSpeed;
    public bool isLooping = true;
    public List<GameObject> wayPoints;
    #endregion

    #region Private Fields
    Transform trans;
    int counter = 0;
    #endregion

	#region Unity Methods
	// Use this for initialization
	void Start () {
        trans = GetComponent<Transform>();
        trans.position = wayPoints[counter].GetComponent<Transform>().position;
        counter++;
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    void OnDrawGizmos()
    {
        for (int i = 0; i < wayPoints.Count; i++)
        {
            if (i == 0)
                Gizmos.color = Color.green;
            else if (i == wayPoints.Count - 1)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.yellow;

            Gizmos.DrawWireCube(wayPoints[i].transform.position, Vector3.one);

            if (i != wayPoints.Count - 1)
                Gizmos.DrawLine(wayPoints[i].transform.position, wayPoints[i + 1].transform.position);
            else
            {
                if (isLooping)
                {
                    Gizmos.DrawLine(wayPoints[i].transform.position, wayPoints[0].transform.position);
                }
            }
        }
    }

    
	#endregion
	
	#region Private Methods
    void Movement()
    {
        if (wayPoints != null)
        {
            if (counter < wayPoints.Count)
            {
                if (trans.position == wayPoints[counter].GetComponent<Transform>().position)
                    counter++;
                else
                    trans.position = Vector3.MoveTowards(trans.position, wayPoints[counter].GetComponent<Transform>().position, movementSpeed * Time.deltaTime);
            }
            else
            {
                if (isLooping)
                    counter = 0;
            }
        }
    }
    #endregion
}