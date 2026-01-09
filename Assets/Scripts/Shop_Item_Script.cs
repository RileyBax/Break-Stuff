using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shop_Item_Script : MonoBehaviour{

    public GameObject shop;
    public GameObject coin;
    public GameController gc;
    public GameObject spriteImage;
    public GameObject propSpawner;
    private int cost;
    private int spriteRand;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        shop = GameObject.Find("Shop Panel");
        gc = GameObject.Find("World").GetComponent<GameController>();
        propSpawner = GameObject.Find("Prop Spawner");
        cost = Random.Range(1, 5);
        spriteRand = Random.Range(0, gc.sprites.Length-1);
        //Debug.Log("Sprite Created: " + spriteRand);

        for (int i = 0; i < cost; i++)
        {
            
            Instantiate(coin, transform).transform.position += new Vector3(i / 5.0f - 1.75f, 4.75f, -1);

        }

        spriteImage.GetComponent<SpriteRenderer>().sprite = gc.sprites[spriteRand];
        spriteImage.transform.localScale *= 0.5f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {

        if(gc.score >= cost){

            gc.addScore(cost * -1);
            Debug.Log(spriteRand);
            propSpawner.SendMessage("spawnPropSprite", spriteRand);
            shop.SendMessage("removeItem", gameObject);
            Destroy(gameObject);
        
        }
    }
}
