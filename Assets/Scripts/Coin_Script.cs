using System;
using UnityEditor.Animations;
using UnityEditor.SearchService;
using UnityEngine;

public class Coin_Script : MonoBehaviour
{

    private Rect target;
    private Rigidbody2D rb;
    private bool hide = false;
    public GameObject world;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // spawn on prop break -> float upwards slow -> pause -> move to corner?
        target = new Rect(UnityEngine.Random.Range(transform.position.x - 1.0f, transform.position.x + 1.0f), UnityEngine.Random.Range(transform.position.y + 1.0f, transform.position.x + 2.0f), 1.0f, 0.5f);
        rb = gameObject.GetComponent<Rigidbody2D>();
        world = GameObject.Find("World");

    }

    // Update is called once per frame
    void Update()
    {

        if(!target.Contains((Vector2) transform.position)) rb.AddForce((target.center - (Vector2) transform.position) * 0.5f);
        else if(!hide) {
            
            Vector2 temp = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));
            target = new Rect(temp.x - 1f, temp.y - 1f, 2, 2);
            hide = true;
        }
        else {

            world.SendMessage("addScore", 1);

            Destroy(gameObject);
        }

        rb.linearVelocity *= 0.99f;

    }
}
