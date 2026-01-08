using Delaunay.Geo;
using UnityEngine;

public class FragCount : MonoBehaviour
{

    private float timer = 0.0f;
    private float maxTime = 5.0f;
    private MeshRenderer mr;

    void Start()
    {
        
        mr = transform.GetComponent<MeshRenderer>();

    }

    void Update()
    {
        
        if(timer <= maxTime) {

            if(timer >= maxTime / 2){
                mr.materials[0].color = new Color(mr.materials[0].color.r, mr.materials[0].color.b, mr.materials[0].color.g, (maxTime - timer) / maxTime);
            }

            timer += Time.deltaTime;
        }
        else Destroy(transform.gameObject);

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Piece") Physics2D.IgnoreCollision(collision.gameObject.GetComponent<PolygonCollider2D>(), gameObject.GetComponent<PolygonCollider2D>());


    }
    
}
