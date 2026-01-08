using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Egg_Controller : MonoBehaviour
{

    public Rigidbody2D propRB;
    private UnityEngine.Vector2 prevVel;
    protected Explodable explodeable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        transform.Rotate(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
        propRB = transform.gameObject.GetComponent<Rigidbody2D>();
        explodeable = transform.gameObject.GetComponent<Explodable>();
        explodeable.extraPoints = Random.Range(2, 4);
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

            explodeable.explode(prevVel);
            
        }

    }

    public void setSprite(Sprite sprite)
    {
        
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        gameObject.GetComponent<BoxCollider2D>().size = new UnityEngine.Vector2(sprite.bounds.size.x, sprite.bounds.size.y);

    }

}
