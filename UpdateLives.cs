using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class UpdateLives : MonoBehaviour
{
    public static int score = 3;

    // Use this for initialization
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Lives: " + score;
    }
    public void incrementScore(int incrementValue)
    {
        score -= incrementValue;
        GetComponent<TextMeshProUGUI>().text = "Lives: " + score;
        if(score == 0)
        {
            SceneManager.LoadScene(2);   //Load the game scene for game over
        }
    }
    public int getScore()
    {
        return score;
    }
    //private int score = 3;

    //public TextMeshProUGUI scoreText;   //display Time on the screen using TextMeshPro object

    //// Use this for initialization
    //void Start()
    //{
    //    GetComponent<TextMeshProUGUI>().text = "Lives: " + this.score;
    //}

    ////Decrements the lives left when hitting missile
    //public void decrementLives()
    //{
    //    this.score --;
    //    scoreText.text = (score).ToString("0");
    //    if (this.score == 0)   //When no time left
    //    {
    //        SceneManager.LoadScene(2);   //Load the game scene for game over

    //    }
    //}

    ////Returns number of lives left
    //public int getScore()
    //{
    //    return this.score;
    //}
}
