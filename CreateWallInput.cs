using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWallInput : MonoBehaviour
{
    [SerializeField]
    private GameObject cut;

    [SerializeField]
    private float cutDestroyTime;

    //private bool dragging = false;
    private Vector2 swipeStart;

    public static List<Vector2> wallPoints = new List<Vector2>();
    public static List<GameObject> connectLines = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && (wallPoints.Count == 0)) //If first input on wall
        {
            //this.dragging = true;
            this.swipeStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //Add position to list
            wallPoints.Add(this.swipeStart);
        }
        else if (Input.GetMouseButtonDown(0) && (wallPoints.Count != 0))    //If not first input, connect with other input
        {
            this.connectInputs();
        }
        if (Input.GetMouseButtonUp(0))  //Once released, destroy all connecting lines
        {
            for (int i = 0; i < connectLines.Count; i++)
            {
                Destroy(connectLines[i]);
            }
        }
    }

    private void connectInputs()
    {
        Vector2 swipeEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Get coordinates of new input
        wallPoints.Add(swipeEnd);   //Add new input to List of points
        for(int i = 0; i < wallPoints.Count; i++)
        {
            GameObject cut = Instantiate(this.cut, this.swipeStart, Quaternion.identity) as GameObject; //Create line game object
            cut.GetComponent<LineRenderer>().SetPosition(0, this.swipeStart);   //Render line on screen
            cut.GetComponent<LineRenderer>().SetPosition(1, swipeEnd);
            Vector2[] colliderPoints = new Vector2[2];
            colliderPoints[0] = new Vector2(0.0f, 0.0f);
            colliderPoints[1] = swipeEnd - this.swipeStart;
            cut.GetComponent<EdgeCollider2D>().points = colliderPoints;
            connectLines.Add(cut.gameObject);   //Add gameobjects to list
        }
    }
}
