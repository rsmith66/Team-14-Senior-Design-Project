using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GetInput : MonoBehaviour
{
    [SerializeField]
    private GameObject cut;

    [SerializeField]
    private GameObject square;

    [SerializeField]
    private float cutDestroyTime;

    private bool dragging = false;
    private Vector2 swipeStart;

    //private string path = "Assets/Resources/data.txt";

    // Update is called once per frame
    void Update()
    {
        BoxCollider2D box = square.GetComponent<BoxCollider2D>();
        if (Input.GetMouseButtonDown(0))    //If mouse button is pressed
        {
            this.dragging = true;
            this.swipeStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            box.enabled = true;
            square.transform.position = new Vector2(this.swipeStart.x, this.swipeStart.y);
        }
        else if (Input.GetMouseButtonUp(0) && this.dragging)    //When the mouse button is not pressed anymore
        {
            //this.connectInputs();
            box.enabled = false;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            box.enabled = false;
        }
    }

    private void connectInputs()
    {
        this.dragging = false;
        Vector2 swipeEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject cut = Instantiate(this.cut, this.swipeStart, Quaternion.identity) as GameObject; //Creates new game object for cut
        cut.GetComponent<LineRenderer>().SetPosition(0, this.swipeStart);
        cut.GetComponent<LineRenderer>().SetPosition(1, swipeEnd);
        Vector2[] colliderPoints = new Vector2[2];  //The two points to collide together
        colliderPoints[0] = new Vector2(0.0f, 0.0f);
        colliderPoints[1] = swipeEnd - this.swipeStart;
        cut.GetComponent<EdgeCollider2D>().points = colliderPoints;
        Destroy(cut.gameObject, this.cutDestroyTime);   //Gets rid of game object
    }
}
