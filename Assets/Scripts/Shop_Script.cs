using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Shop_Script : MonoBehaviour
{

    public List<GameObject> shopList = new List<GameObject>();
    private float newItemTimer = 0.0f;
    public GameObject shopItemPrefab;
    public GameObject gc;
    private GameController gcScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        newStoreItem();
        gcScript = gc.GetComponent<GameController>();
        
    }

    // Update is called once per frame
    void Update()
    {

        // create shop items every few secounds, add to shop list, slide item from left -> right side of shop
        if(newItemTimer <= Math.Max(1.0f, 5.0f - gcScript.getMultiP())) newItemTimer += Time.deltaTime;
        else
        {
            
            newItemTimer = 0.0f;

            // spawn new item
            if(shopList.Count < 4 + gcScript.getMultiP()) newStoreItem();

        }

        for (int i = 0; i < shopList.Count; i++)
        {

            float xTarget = 1.25f - (1.25f * i);

            if(shopList[i].transform.position.x < xTarget) shopList[i].transform.Translate(Vector2.right * Time.deltaTime * 2.0f, Space.World);

        }
        
    }

    void newStoreItem()
    {
        
        GameObject shopItem = Instantiate(shopItemPrefab, gameObject.transform);
        shopItem.transform.position -= new Vector3(8, 0, 0);
        
        shopList.Add(shopItem);

    }

    void removeItem(GameObject item)
    {
        
        shopList.Remove(item);

    }
}
