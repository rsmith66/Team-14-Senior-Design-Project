using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    public float timeLeft; //Total time for game

    public static float roundNumber = 1;

    public bool isGameOver;

    public TextMeshProUGUI timeText;   //display Time on the screen using TextMeshPro object

    void Update()
    {
        //Decrement time left every second
        timeLeft -= Time.deltaTime;
        timeText.text = (timeLeft).ToString("0");
        if (timeLeft < 0)   //When no time left
        {
           //Get rid of text
           //Launch function that displays pattern with round number as input
           //Continually Invoke checkHolds function
        }
    }
}
