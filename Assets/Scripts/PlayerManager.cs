using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : GenericManager<PlayerManager>
{ 
    private bool isAlive;

    public bool IsAlive() { return isAlive; }

    private void Awake()
    {
        // value change so that droen spawning isn't a runon
        while (Time.time < 30f)
        {
            isAlive = true;
        }        
    }

    // TODO player health > 0, true, else isAlive is false
}
