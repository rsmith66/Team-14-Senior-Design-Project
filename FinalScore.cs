using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FinalScore : MonoBehaviour
{
    public int score = 0;

    public TextMeshProUGUI scoreTexts;

    // Start is called before the first frame update
    void Start()
    {
        float score = CreateHolds.score;
        scoreTexts.text = score.ToString("0");
    }
}
