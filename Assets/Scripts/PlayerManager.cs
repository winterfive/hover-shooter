using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : GenericManager<PlayerManager> { 

    public int playerScore;
    public int playerHealth;
    public delegate void ChangeScore();
    public static ChangeScore OnChangeScore;
    public delegate void ChangePlayerHealth();
    public static ChangePlayerHealth OnChangePlayerHealth;


    private void CheckPlayerDamage(GameObject go)
    {
        // Check projectile
        // If missle, call this
        // If drone bomb, call this
    }
    

    private void UpdateScore()
    {
        // Listens to ProjectileActions for missleHit
        // Depending on type of projectile has hit, adjust score value
        if (OnChangeScore != null)
        {
            OnChangeScore();
        }
    }

    private void UpdateHealth()
    {
        // Listens to ProjectileManager for missleHit
        // Updates health depending on type of projectile that has hit player
        if (OnChangePlayerHealth != null)
        {
            OnChangePlayerHealth();
        }
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
