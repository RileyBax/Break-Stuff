using UnityEngine;
using System.Collections.Generic;

public class Fragment_Handler : ExplodableAddon
{
    
    public FragCount FragC;

    public override void OnFragmentsGenerated(List<GameObject> fragments)
    {
        
        foreach(GameObject fragment in fragments)
        {
            
            // TODO: ADD THE RIGIDBODY FROM THE MAIN GAMEOBJECT TO CONTINUE THE VELOCITY
            fragment.AddComponent<FragCount>();

        }

    }

}
