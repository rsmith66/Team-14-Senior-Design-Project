using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMissiles : MonoBehaviour
{
    [SerializeField]
    private GameObject missileToSpawn;

    [SerializeField]
    private float spawnInterval, objectMinX, objectMaxX, objectY;

    [SerializeField]
    private Sprite[] objectSprites;

    private void spawnObject()
    {
        //Creates new game object using missile
        GameObject newObject = Instantiate(this.missileToSpawn);
        //Sets random location of missile
        newObject.transform.position = new Vector2(Random.Range(this.objectMinX, this.objectMaxX), this.objectY);
        //Chooses the sprite to display
        Sprite objectSprite = objectSprites[Random.Range(0, this.objectSprites.Length)];
        newObject.GetComponent<SpriteRenderer>().sprite = objectSprite;
    }

    void Start()
    {
        InvokeRepeating("spawnObject", this.spawnInterval, this.spawnInterval);
    }

}
