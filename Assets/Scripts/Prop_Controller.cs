using Unity.VisualScripting;
using UnityEngine;

public class Egg_Controller : MonoBehaviour
{

    public Rigidbody2D propRB;
    private float prevVel = 0.0f;
    protected Explodable explodeable;
    private bool exploded = false;
    private float clearT = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        transform.Rotate(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
        propRB = transform.gameObject.GetComponent<Rigidbody2D>();
        explodeable = transform.gameObject.GetComponent<Explodable>();
        explodeable.extraPoints = Random.Range(2, 4);

    }

    // Update is called once per frame
    void Update()
    {

        if(propRB) prevVel = propRB.linearVelocity.magnitude;


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(prevVel > 5.0f) {

            explodeable.fragmentInEditor();
            explodeable.explode();
            
        }

    }

}
