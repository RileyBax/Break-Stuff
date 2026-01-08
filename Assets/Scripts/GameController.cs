using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject egg;
    Rigidbody2D eggRB;
    GameObject eggObject;
    [Range(0.0f, 10.0f)]
    public float fMulti = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider != null)
            {
                
                // Drag egg around
                //GameObject egg = hit.collider.gameObject.transform.parent.gameObject;

                eggRB = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                eggObject = hit.collider.gameObject;
                //Debug.Log(eggRB);

            }
            else Instantiate(egg).transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }

        if (Input.GetMouseButton(0))
        {
            
            if(eggRB){

                Vector2 force = (((Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2) eggObject.transform.position) * 0.8f);
                eggRB.gravityScale = 0;
                eggRB.AddForce(force * fMulti); // check if egg object is near mouse pos, dont add force if true

            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            
            if(eggRB){

                eggRB.gravityScale = 1;
                eggRB = null;
                eggObject = null;

            }

        }

        if (eggRB)
        {
            
            eggRB.linearVelocity *= new Vector2(0.999f, 0.999f);

        }

    }
}
