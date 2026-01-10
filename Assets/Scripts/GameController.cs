using System;
using TMPro;
using UnityEngine;

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
    public const float multiTimerMax = 5.0f;
    private int multiP = 0;
    public TextMeshProUGUI multiText;
    public GameObject multiBar;
    private float multiBarFull;
    public GameObject multiBox;
    public AudioSource soundController;
    public AudioClip coinSound;
    public AudioClip[] crashSound = new AudioClip[4];
    public AudioClip buySound;
    public AudioClip wrongSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        // load sprites
        sprites = Resources.LoadAll<Sprite>("Sprites/sprites");
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

        if(multiTimer < multiTimerMax) {

            if(multiP > 10) multiTimer += Time.deltaTime * (multiP / 10.0f);
            else multiTimer += Time.deltaTime;
            
        }
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
            multiBar.GetComponent<RectTransform>().sizeDelta = new Vector2(multiBarFull * (multiTimerMax - multiTimer) * 2 / 10.0f, 30);

            float sinWave = (float) Mathf.Sin(multiTimer * (multiP / 2.0f)) * (multiP / 2.0f);

            multiBox.transform.localRotation = Quaternion.Euler(0, 0, sinWave);

        }
        else multiText.text = "";

    }

    public void addScore(int amount)
    {
        
        score += amount;
        scoreText.text = score.ToString();

        if(amount > 0) soundController.PlayOneShot(coinSound);

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

    public void playBreak()
    {
        
        soundController.PlayOneShot(crashSound[UnityEngine.Random.Range(1, crashSound.Length-1)]);

    }

    public void playBuy()
    {
        
          soundController.PlayOneShot(buySound);

    }

    public void playWrong()
    {
        
          soundController.PlayOneShot(wrongSound);

    }
}
