using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FinalScore : MonoBehaviour
{
    public int score = 300;

    public TextMeshProUGUI scoreTexts;

    // Start is called before the first frame update
    void Start()
    {
        GameObject scoreText = GameObject.Find("Lives");
        float timeLeft = Timer.scoreTime;
        int missilesLeft = UpdateLives.score;
        if (missilesLeft != 0)
        {
            scoreTexts.text = ((missilesLeft * 50) + (45 * 10)).ToString("0");
        }
        else
        {
            scoreTexts.text = ((45 - timeLeft) * 10).ToString("0");
        }
    }
}
