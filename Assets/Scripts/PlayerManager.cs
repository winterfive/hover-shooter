using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public int playerScore;
    public int playerHealth;
    

    private void UpdateScore()
    {
        // Listens to ProjectileActions for missleHit
        // Depending on type of projectile has hit, adjust score value
        // Broadcast change in score value
    }

    private void UpdateHealth()
    {
        // Listens to ProjectileManager for missleHit
        // Updates health depending on type of projectile that has hit player
        // Broadcast change in health value
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
