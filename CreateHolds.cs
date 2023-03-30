using System.Collections;
using System.Collections.Generic;
using System;
using Random = System.Random;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class CreateHolds : MonoBehaviour
{
    private List<GameObject> cuts;  //List to hold game objects of cuts to represented where the body of user is

    public GameObject[] holds;  //Creates virual wall of game objects for holds

    public GameObject RoundText;

    public GameObject ReadyText;

    public GameObject PayAttention;

    public GameObject Score;

    public Sprite square;   //Sprite for the hold

    private string path = "Assets/calibration.txt"; //Path to calibration file

    private string dataPath = "Assets/data.txt";    //Path to file with holds being pressed

    private int[,] oldHolds = new int[16, 4];

    public bool firstCall = true;

    private int roundNumber = 1;

    private int numHolds;

    private List<int> rightPattern = new List<int>();

    private List<int> chosenPattern = new List<int>();

    private Color[] colors = { Color.blue, Color.green, Color.red, Color.yellow };

    private List<Color> rightColors = new List<Color>();

    private SpriteRenderer currentSprite;

    public static int score;

    private int numRight;

    public AudioClip CSharp;    //All audio sounds

    public AudioClip E4;

    public AudioClip E5;

    public AudioClip A;

    public GameObject ScoreText;    //Object with audio source on it

    private AudioSource sound;

    private List<AudioClip> notes = new List<AudioClip>();

    private List<AudioClip> rightSounds = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        rightColors.Clear();    //Make sure all these lists are empty upon start
        rightPattern.Clear();
        rightSounds.Clear();
        sound = ScoreText.GetComponent<AudioSource>();
        notes.Add(E4);  //Add notes to the audio source object
        notes.Add(CSharp);
        notes.Add(A);
        notes.Add(E5);
        score = 0;
        //Read from text file from calibration phase and store in calibration matrix
        string calibrationFile = File.ReadAllText(path);    //Gets the contents of calibration text file in string
        int i = 0, j = 0, k = 0;
        numHolds = File.ReadAllLines(path).Length; //Gets the number of rows in text file
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
            //renderer.enabled = false;   //Disables sprite renderer
            holds[k].transform.localScale = new Vector3(0.2f, 0.2f, 1);
            holds[k].transform.position = new Vector2(calibrationData[k, 3], calibrationData[k, 4]);  //Sets position of hold game object to be that calibration data
            BoxCollider2D box = holds[k].AddComponent<BoxCollider2D>(); //Add box collider and disable it
            box.enabled = false;
            k++;
        }
        StartCoroutine(roundChange());    //Disables round text and displays Ready? Text
    }

    //Function that takes down round number and turns on ready text
    IEnumerator roundChange()
    {
        yield return new WaitForSeconds(3);
        RoundText.SetActive(false);     //Turn off round text
        ReadyText.SetActive(true);     //Turn on Ready text
        StartCoroutine(readyChange());    //Disables Ready? text and displays Pay Attention!
    }

    //Function that takes down ready text and turns on pay attention text
    IEnumerator readyChange()
    {
        yield return new WaitForSeconds(2);
        ReadyText.SetActive(false);     //Turn off round text
        StartCoroutine(displayPattern()); //Begins displaying pattern on hold
    }

    //Function that will begin displaying random pattern for Simon game
    IEnumerator displayPattern()
    {
        PayAttention.SetActive(true);     //Turn on Starting text
        yield return new WaitForSeconds(2);
        PayAttention.SetActive(false);     //Turn on Starting text
        //Choose random hold and add to list
        Random rnd = new Random();  //Generate random number
        int randomHold = rnd.Next(0, numHolds);
        rightPattern.Add(randomHold); //Add to list of holds chosen for computer pattern
        //Choose color and add to color list
        int nextColor = (roundNumber-1) % 4;
        rightColors.Add(colors[nextColor]);
        //Choose audio snippet and add to list
        rightSounds.Add(notes[nextColor]);
        //Go through list of chosen holds and change sprite to color, play audio clip, and then change back
        for(int i = 0; i < rightPattern.Count; i++)
        {
            SpriteRenderer holdSprite = holds[rightPattern[i]].GetComponent<SpriteRenderer>();
            holdSprite.color = rightColors[i];
            currentSprite = holdSprite;
            AudioClip audio = rightSounds[i];
            sound.PlayOneShot(audio, 1.0f);
            yield return new WaitForSeconds(1);
            sound.Stop();
            changeHoldColorBack();
        }
        numRight = 0;   //running total for number of holds guessed correctly in pattern
        InvokeRepeating("checkHolds", 0.0f, 0.05f); //Repeatedly check for user inputs
    }

    void changeHoldColorBack()
    {
        currentSprite.color = Color.white;
    }

    IEnumerator waitForASec()
    {
        yield return new WaitForSeconds(5);
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
        int numColumns = holdData.GetLength(1);
        for (int m = 0; m < numRows; m++)
        {
            for (int n = 0; n < numColumns; n++)
            {
                if((oldHolds[m, n] == 1 && holdData[m, n] == 1))    //If hold is still pressed
                {
                    //Debug.Log("Button is still pressed");   //Do nothing
                }
                else if (holdData[m, n] == 1) //If a hold is pressed
                {
                    if (currentHold != rightPattern[numRight])   //If hold is not same as pattern
                    {
                        //SpriteRenderer holdSprite = holds[currentHold].GetComponent<SpriteRenderer>();  //Turn hold red
                        //holdSprite.color = Color.black;
                        SceneManager.LoadScene(2);  //Go to Game Over scene
                    }
                    else   //If hold is the same as pattern
                    {
                        //Debug.Log("Correct!");
                        SpriteRenderer holdSprite = holds[currentHold].GetComponent<SpriteRenderer>();  //Turn hold color of pattern
                        holdSprite.color = rightColors[numRight];
                        if (oldHolds[m, n] == 0 && holdData[m, n] == 1)  //If statement to make sure this is only updated once
                        {   
                            AudioClip audio = rightSounds[numRight];    //Play correct sound
                            sound.PlayOneShot(audio, 1.0f);
                            numRight++; //Increase number of holds in pattern guessed right
                        }
                    }
                }
                else if ((oldHolds[m, n] == 1 && holdData[m, n] == 0))   //For holds that are not longer pressed
                {
                    SpriteRenderer renderer = holds[currentHold].GetComponent<SpriteRenderer>();    //Turn color of hold back to white
                    renderer.color = Color.white;
                    sound.Stop();   //Turn off sound
                    if (numRight == rightPattern.Count)
                    {
                        score = score + 100; //Increase score by 100
                        Score.GetComponent<TextMeshProUGUI>().text = score.ToString("0");   //Update text on screen
                        roundNumber++;
                        StartCoroutine(startNextRound());
                    }
                }
                currentHold++;
            }
        }
        oldHolds = holdData;    //Assign the current holds being pressed to be the previous holds for when called next
    }

    IEnumerator startNextRound()
    {
        CancelInvoke();
        yield return new WaitForSeconds(1);
        for (int i = 0; i < holds.Length; i++)   //Turn color of hold back to white
        {
            SpriteRenderer renderer = holds[i].GetComponent<SpriteRenderer>();    
            renderer.color = Color.white;
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(displayPattern());
    }
}
