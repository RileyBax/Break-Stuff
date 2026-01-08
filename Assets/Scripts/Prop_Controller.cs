using System.Numerics;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        transform.Rotate(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
        propRB = transform.gameObject.GetComponent<Rigidbody2D>();
        explodeable = transform.gameObject.GetComponent<Explodable>();
        explodeable.extraPoints = Random.Range(5, 10);
        explodeable.shatterType = Explodable.ShatterType.Voronoi;

        explodeable.fragmentInEditor();

    }

    // Update is called once per frame
    void Update()
    {

        if(propRB) prevVel = propRB.linearVelocity;


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(prevVel.magnitude > 5.0f) {

            if (!coinSpawned)
            {
                
                Instantiate(coin).transform.position = transform.position;
                coinSpawned = true;

            }

            if(collision.gameObject.tag == "Wall") explodeable.explode(prevVel, true);
            else explodeable.explode(prevVel, false);

            

        }

    }

    public void setSprite(Sprite sprite)
    {
        
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        gameObject.GetComponent<BoxCollider2D>().size = new UnityEngine.Vector2(sprite.bounds.size.x, sprite.bounds.size.y);

    }

}
