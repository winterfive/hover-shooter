using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : GenericManager<PlayerManager>
{ 
    private bool _isAlive = true;

    public bool IsAlive() { return _isAlive; }

    private void Update()
    {
        if (Time.time > 20f)
        {
            _isAlive = false;
        }
    }

    // TODO player health > 0, true, else isAlive is false
}
