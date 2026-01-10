using System;
using System.Collections.Generic;
using Delaunay;
using Unity.VisualScripting;
using UnityEngine;

public class Egg_Controller : MonoBehaviour
{

    public Rigidbody2D propRB;
    private UnityEngine.Vector2 prevVel;
    protected Explodable explodeable;
    public GameObject coin;
    private bool coinSpawned = false;
    public GameObject gc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        gc = GameObject.Find("World");

        transform.Rotate(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10));
        
        initiateProp();

    }

    // Update is called once per frame
    void Update()
    {

        if(propRB) prevVel = propRB.linearVelocity;

        if(transform.position.y < -10) Destroy(gameObject);


    }

    void initiateProp()
    {
        
        propRB = transform.gameObject.GetComponent<Rigidbody2D>();
        explodeable = transform.gameObject.GetComponent<Explodable>();
        explodeable.extraPoints = UnityEngine.Random.Range(5, 10);
        explodeable.shatterType = Explodable.ShatterType.Voronoi;

        explodeable.fragmentInEditor();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(prevVel.magnitude > 8.0f) {

            gc.SendMessage("playBreak");

            if (!coinSpawned)
            {
                
                for(int i = 0; i < gc.GetComponent<GameController>().getMultiP() + 1; i++)
                {
                    
                    GameObject tempPrefab = Instantiate(coin);
                    tempPrefab.transform.position = transform.position;

                }

                coinSpawned = true;

            }

            gc.SendMessage("setMulti", 1);

            if(collision.gameObject.tag == "Wall") explodeable.explode(prevVel, true);
            else explodeable.explode(prevVel, false);

        }
        else if(collision.gameObject.tag == "Piece") Physics2D.IgnoreCollision(collision.gameObject.GetComponent<PolygonCollider2D>(), gameObject.GetComponent<BoxCollider2D>());

    }

    public void setSprite(Sprite sprite)
    {
        
        //Debug.Log("Prop: " + sprite);

        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        gameObject.GetComponent<BoxCollider2D>().size = new UnityEngine.Vector2(sprite.bounds.size.x, sprite.bounds.size.y);

        //initiateProp();

    }

}
