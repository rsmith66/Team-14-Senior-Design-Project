using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreateHolds : MonoBehaviour
{
    public GameObject cut;  //Properties of cut

    private List<GameObject> cuts;  //List to hold game objects of cuts to represented where the body of user is

    public GameObject[] holds;

    public Sprite square;   //Sprite for the hold

    private string path = "Assets/calibration.txt"; //Path to calibration file

    private string dataPath = "Assets/data.txt";    //Path to file with holds being pressed

    private int[,] oldHolds = new int[16, 4];

    public bool firstCall = true;

    // Start is called before the first frame update
    void Start()
    {
        //Read from text file from calibration phase and store in calibration matrix
        string calibrationFile = File.ReadAllText(path);    //Gets the contents of calibration text file in string
        int i = 0, j = 0, k = 0;
        int numHolds = File.ReadAllLines(path).Length; //Gets the number of rows in text file
        int[,] calibrationData = new int[numHolds, 5];
        foreach (var row in calibrationFile.Split('\n'))
        {
            j = 0;
            foreach (var col in row.Trim().Split(' '))
            {
                calibrationData[i, j] = int.Parse(col.Trim());  //Load the calibration matrix with ints converted from text file
                j++;
            }
            i++;
        }

        //For each row of matrix, get x and y and create game object at that location
        holds = new GameObject[numHolds];
        while (k < numHolds)
        {
            //Create game object for hold[k]
            holds[k] = new GameObject("Hold");
            holds[k].tag = "Hold";
            SpriteRenderer renderer = holds[k].AddComponent<SpriteRenderer>();    //Sets the sprite to be a square
            renderer.sprite = square;
            renderer.enabled = false;   //Disables sprite renderer
            holds[k].transform.localScale = new Vector3(0.2f, 0.2f, 1);
            holds[k].transform.position = new Vector2(calibrationData[k,3], calibrationData[k,4]);  //Sets position of hold game object to be that calibration data
            BoxCollider2D box = holds[k].AddComponent<BoxCollider2D>(); //Add box collider and disable it
            box.enabled = false;
            k++;
        }
        cuts = new List<GameObject>();
        InvokeRepeating("checkHolds", 0.0f, 0.05f);  //Calls the checkHolds function immediately after setup and calls it every 0.05 seconds

    }

    void checkHolds()
    {
        //Read from text file with hold update values and store in updated hold matrix
        string dataFile = File.ReadAllText(dataPath);    //Gets the contents of calibration text file in string
        int i = 0, j = 0;
        int numRows = File.ReadAllLines(path).Length; //Gets the number of rows in text file
        int[,] holdData = new int[numRows, 4];
        foreach (var row in dataFile.Split('\n'))
        {
            j = 0;
            foreach (var col in row.Trim().Split(' '))
            {
                holdData[i, j] = int.Parse(col.Trim());  //Load the data matrix with ints converted from text file
                j++;
            }
            i++;
        }

        //Look through data matrix and find all holds that are being pressed and turn Game Object box collider on
        if (firstCall)   //If first time called, set current hold matrix to previous one
        {
            oldHolds = holdData;
            firstCall = false;
        }
        int currentHold = 0;    //running total for number of hold being analyzed
        List<GameObject> activeHolds = new List<GameObject>();
        for(int m = 0; m < numRows; m++)
        {
            for(int n = 0; n < 4; n++)
            {
                if(holdData[m, n] == 1) //Enable the box collider for the pressed hold
                {
                    BoxCollider2D box = holds[currentHold].GetComponent<BoxCollider2D>();
                    box.enabled = true;
                    SpriteRenderer renderer = holds[currentHold].GetComponent<SpriteRenderer>();    //Activate the sprite for the hold
                    renderer.enabled = true;
                    activeHolds.Add(holds[currentHold]);    //Add game objects of active hold to list
                    //renderer.enabled = false;
                }
                else if(oldHolds[m,n] == 1 && holdData[m,n] == 0)   //Turn off the box collider for holds that are not longer pressed
                {
                    BoxCollider2D box = holds[currentHold].GetComponent<BoxCollider2D>();
                    box.enabled = false;
                    SpriteRenderer renderer = holds[currentHold].GetComponent<SpriteRenderer>();    //Deactivate the sprite for the hold
                    renderer.enabled = false;
                }
                currentHold++;
            }
        }

        //Go through active holds array and draw GameObject line between each active hold
        if (activeHolds.Count > 1)
        {
            int totalActiveHolds = activeHolds.Count - 1;   //Number of iterations for outer for loop that can be changed to make less lines
            int p = 0;
            for (int o = 0; o < totalActiveHolds; o++) //for current hold
            {
                p++;
                int g = p;
                Vector2 holdPosition = activeHolds[o].transform.position;
                Vector2 originalPosition = holdPosition;
                while(g<activeHolds.Count)
                {
                    Vector2 otherHoldPosition = activeHolds[g].transform.position;
                    if (holdPosition.x < otherHoldPosition.x)
                    {
                        holdPosition.x = holdPosition.x + 0.5f;   //Shift x values of holds a little
                        otherHoldPosition.x = otherHoldPosition.x - 0.5f;
                    }
                    else if(holdPosition.x > otherHoldPosition.x)
                    {
                        holdPosition.x = holdPosition.x - 0.5f;
                        otherHoldPosition.x = otherHoldPosition.x + 0.5f;
                    }
                    if (holdPosition.y > otherHoldPosition.y)
                    {
                        holdPosition.y = holdPosition.y - 0.5f;   //Shift y values of holds a little
                        otherHoldPosition.y = otherHoldPosition.y + 0.5f;
                    }
                    else if (holdPosition.y < otherHoldPosition.y)
                    {
                        holdPosition.y = holdPosition.y + 0.5f;   //Will this ever be entered?? Lines get drawn down not up
                        otherHoldPosition.y = otherHoldPosition.y - 0.5f;
                    }
                    GameObject cut = Instantiate(this.cut, holdPosition, Quaternion.identity) as GameObject; //Creates new game object for cut
                    cut.tag = "Hold";
                    cut.GetComponent<LineRenderer>().SetPosition(0, holdPosition);  //Set position for the beginning and end point of the line
                    cut.GetComponent<LineRenderer>().SetPosition(1, otherHoldPosition);
                    LineRenderer line = cut.GetComponent<LineRenderer>();   //Turn off the display of the line
                    //line.enabled = false;
                    cut.transform.localScale = new Vector3(0.9f, 0.9f, 1);  //Scale down line so the collider does not touch the collider of hold
                    Vector2[] colliderPoints = new Vector2[2];  //Set points for the 2D Edge Collider with missiles
                    colliderPoints[0] = new Vector2(0.0f, 0.0f);
                    colliderPoints[1] = otherHoldPosition - holdPosition;
                    cut.GetComponent<EdgeCollider2D>().points = colliderPoints;
                    cuts.Add(cut);
                    g++;
                    holdPosition = originalPosition;  //Reset coordinate of hold to be the original
                }
            }
            Invoke("destroyLines", 0.04f);   //Gets rid of game object right before the function gets called again in the InvokeRepeating call above
        }
        oldHolds = holdData;    //Assign the current holds being pressed to be the previous holds for when called next
    }

    //Function to destroy the game object for each line
    void destroyLines()
    {
        for(int i = 0; i < cuts.Count; i++)
        {
            Destroy(cuts[i]);
        }
    }
}
