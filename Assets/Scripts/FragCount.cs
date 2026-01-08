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

            if(timer >= maxTime / 2) mr.materials[0].color = new Color(mr.materials[0].color.r, mr.materials[0].color.b, mr.materials[0].color.g, (maxTime - timer) / maxTime);

            timer += Time.deltaTime;
        }
        else Destroy(transform.gameObject);

    }

}
