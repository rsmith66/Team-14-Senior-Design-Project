using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    //Min and Max values for x and y speed
    [SerializeField]    //Allows values to be stored for later
    private float minXSpeed, maxXSpeed, minYSpeed, maxYSpeed;

    //Timer for when to destroy missile
    [SerializeField]
    private float destroyTime;

    // Start is called before the first frame update
    void Start()
    {
        //Chooses random value from min/max x and y values
        var missile = this.gameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(minXSpeed, maxXSpeed), Random.Range(minYSpeed, maxYSpeed));

    }
}
