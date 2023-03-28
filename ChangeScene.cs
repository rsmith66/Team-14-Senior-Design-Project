using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public bool restart = true;
    void Update()
    {
        if(restart)
        {
            if (Input.GetMouseButtonDown(0))    //If Play again button is pressed
            {
                UpdateLives.score = 3;  //Reset lives to 3
                SceneManager.LoadScene(3);  //Go to Start scene
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))    //If Play again button is pressed
            {
                SceneManager.LoadScene(0);  //Go to Title scene
            }
        }
        
    }
}
