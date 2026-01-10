using System;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject prop;
    Rigidbody2D propRB;
    GameObject propObject;
    private float fMulti = 5f;
    public Sprite[] sprites;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public GameObject propSpawner;
    public bool debugSpawn = false;
    public float multiTimer = 0.0f;
    private int multiP = 0;
    public TextMeshProUGUI multiText;
    public GameObject multiBar;
    private float multiBarFull;
    public GameObject multiBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        // load sprites
        sprites = Resources.LoadAll<Sprite>("Sprites/Bathroom_Demo");
        propSpawner.SendMessage("setSprites", sprites);
        multiBarFull = multiBar.GetComponent<RectTransform>().sizeDelta.x;

        multiBar.transform.position = multiBox.transform.position - new Vector3(0, 0.2f, 0);;
        multiText.transform.position = multiBox.transform.position + new Vector3(0, 0.2f, 0);

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

                propRB = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                propObject = hit.collider.gameObject;
                
                if(hit.collider.tag == "Shop Item") hit.collider.SendMessage("OnClick");

            }
            else if(debugSpawn) {
                
                GameObject temp = Instantiate(prop);
                temp.transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
                temp.SendMessage("setSprite", sprites[UnityEngine.Random.Range(0, sprites.Length - 1)]);

            }

        }

        if (Input.GetMouseButton(0))
        {
            
            if(propRB){

                Vector2 force = (((Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2) propObject.transform.position) * 0.8f);
                propRB.gravityScale = 0;
                propRB.AddForce(force * fMulti); // check if egg object is near mouse pos, dont add force if true

            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            
            if(propRB){

                propRB.gravityScale = 1;
                propRB = null;
                propObject = null;

            }

        }

        if (propRB)
        {
            
            propRB.linearVelocity *= new Vector2(0.99f, 0.99f);

        }

        if(multiTimer < 10.0f) multiTimer += Time.deltaTime;
        else
        {
            
            multiP = 0;
            multiTimer = 0.0f;

        }

        if(multiP > 1)
        {
            
            String temp = multiP + "x";

            for (int i = 0; i < multiP; i++)
            {
                
                temp += "!";

            }

            multiText.text = temp;

            if(!multiBar.activeSelf) multiBar.SetActive(true);
            multiBar.GetComponent<RectTransform>().sizeDelta = new Vector2(multiBarFull * (10.0f - multiTimer) / 10.0f, 30);

            float sinWave = (float) Mathf.Sin(multiTimer * (multiP / 2.0f)) * (multiP / 2.0f);

            multiBox.transform.localRotation = Quaternion.Euler(0, 0, sinWave);

        }
        else multiText.text = "";

    }

    public void addScore(int amount)
    {
        
        score += amount;
        scoreText.text = score.ToString();

    }

    public void setMulti(int amount)
    {
        
        multiP += amount;
        multiTimer = 0.0f;

    }

    public int getMultiP()
    {
        
        return multiP;

    }
}
