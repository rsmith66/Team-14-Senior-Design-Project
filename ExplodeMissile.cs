using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeMissile : MonoBehaviour
{
    public Sprite explosion;

    public Sprite missile;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hold")
        {
            ChangeSprite(explosion);    //Changes missile to explosion
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();    //Freezes the explosion in place
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            BoxCollider2D boxC = GetComponent<BoxCollider2D>();    //Turns off the Box Collider
            boxC.enabled = false;
            GameObject scoreText = GameObject.Find("Lives");
            scoreText.GetComponent<UpdateLives>().incrementScore(1);    //Decrements lives by one
            Invoke("turnInvisible", 1);
        }
        //else   //If two missiles collide into each other, both missiles explode
        //{
        //    ChangeSprite(explosion);    //Changes missile to explosion
        //    Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();    //Freezes the explosion in place
        //    rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        //    BoxCollider2D boxC = GetComponent<BoxCollider2D>();    //Turns off the Box Collider
        //    boxC.enabled = false;
        //    Invoke("turnInvisible", 1);
        //}
    }

    void turnInvisible()
    {
        this.gameObject.transform.position = new Vector2(500, -150);
        Invoke("changeBack", 3);
    }

    void changeBack()
    {
        ChangeSprite(missile);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        this.gameObject.GetComponent<Renderer>().enabled = true;
        BoxCollider2D boxC = GetComponent<BoxCollider2D>(); //Turns back on the Box Collider
        boxC.enabled = true;
    }

    void ChangeSprite(Sprite newSprite)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = newSprite;
    }
}
