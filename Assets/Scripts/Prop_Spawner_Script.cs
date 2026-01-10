using System;
using System.Collections.Generic;
using UnityEngine;

public class Prop_Spawner_Script : MonoBehaviour
{

    public GameObject prop;
    private Sprite[] sprites;
    public float orderTimer = 0.0f;
    public float spawnTimer = 0.0f;
    public List<int> orders = new List<int>();
    private bool firstSpawn = true;
    public GameObject gc;
    private GameController gcScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        gcScript = gc.GetComponent<GameController>();

    }

    // Update is called once per frame
    void Update()
    {

        if(firstSpawn) {
            spawnPropSprite(UnityEngine.Random.Range(0, sprites.Length-1));
            firstSpawn = false;
        }

        if(orders.Count > 1 && orderTimer >= Math.Max(1.0f, 5.0f - gcScript.getMultiP() / 2.0f))
        {
            
            // spawn prop
            spawnProp();
            

        }
        else if(orders.Count <= 1 && spawnTimer >= 5.0f)
        {
            
            spawnPropSprite(UnityEngine.Random.Range(0, sprites.Length-1));
            spawnTimer = 0.0f;

        }
        
        orderTimer += Time.deltaTime;
        spawnTimer += Time.deltaTime;

    }

    void spawnProp()
    {
        
        Vector2 force = new Vector2(transform.position.x - 8, transform.position.y + 5);
        GameObject newProp = Instantiate(prop);

        newProp.SendMessage("setSprite", sprites[orders[0]]);
        orders.Remove(orders[0]);
        newProp.transform.position = transform.position;
        newProp.GetComponent<Rigidbody2D>().AddForce(force * UnityEngine.Random.Range(50, 60));

        orderTimer = 0.0f;

    }

    void spawnPropSprite(int spriteNum)
    {

        // create random vector2 then add force to instantiated prop
        orders.Add(spriteNum);

    }

    void setSprites(Sprite[] sprites)
    {
        
        this.sprites = sprites;

    }

}
