using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public bool restart = true;
    void Update()
    {
        if (restart)
        {
            if (Input.GetMouseButtonDown(0))    //If Restart button is pressed
            {
                SceneManager.LoadScene(0);  //Go to Title scene
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))    //If Start button is pressed
            {
                SceneManager.LoadScene(1);  //Go to Game scene
            }
        }

    }
}
