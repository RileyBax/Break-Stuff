using UnityEngine;

public class Prop_Spawner_Script : MonoBehaviour
{

    public GameObject prop;
    public float spawnTimer = 0.0f;
    private Sprite[] sprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(spawnTimer < 5.0f) spawnTimer += Time.deltaTime;
        else
        {
            
            spawnProp();
            spawnTimer = 0.0f;

        }

    }

    void spawnProp()
    {
        
        // create random vector2 then add force to instantiated prop
        Vector2 force = new Vector2(transform.position.x - 8, transform.position.y + 5);

        GameObject newProp = Instantiate(prop);

        newProp.SendMessage("setSprite", sprites[UnityEngine.Random.Range(0, sprites.Length-1)]);
        newProp.transform.position = transform.position;
        newProp.GetComponent<Rigidbody2D>().AddForce(force * Random.Range(40, 60));

    }

    void spawnProp(int spriteNum)
    {
        
        // create random vector2 then add force to instantiated prop
        Vector2 force = new Vector2(transform.position.x - 8, transform.position.y + 5);

        GameObject newProp = Instantiate(prop);

        newProp.SendMessage("setSprite", sprites[spriteNum]);
        newProp.transform.position = transform.position;
        newProp.GetComponent<Rigidbody2D>().AddForce(force * Random.Range(40, 60));

    }

    void setSprites(Sprite[] sprites)
    {
        
        this.sprites = sprites;

    }

}
