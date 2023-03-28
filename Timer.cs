using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    public static List<int> sceneList = new List<int>();

    public float timeLeft; //Total time for game

    public static float scoreTime;

    public bool isGameOver;

    public TextMeshProUGUI timeText;   //display Time on the screen using TextMeshPro object

    void Update()
    {
        //Decrement time left every second
        timeLeft -= Time.deltaTime;
        scoreTime = timeLeft;
        timeText.text = (timeLeft).ToString("0");
        if (timeLeft < 0)   //When no time left
        {
            if (isGameOver)
            {
                sceneList.Add(2);
                SceneManager.LoadScene(2);   //Load the game scene for game over
            }
            else
            {
                sceneList.Add(0);
                sceneList.Add(1);
                SceneManager.LoadScene(1);
            }
            
            
        }
    }
}
