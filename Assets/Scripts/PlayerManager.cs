using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : GenericManager<PlayerManager> { 

    public int playerHealth;
    public delegate void UpdatePlayerScore();
    public static UpdatePlayerScore OnUpdatePlayerScore;
    public delegate void UpdatePlayerHealth();
    public static UpdatePlayerHealth OnUpdatePlayerHealth;
    public ProjectileActions projectileActions;

    private int playerScore;


    private void Awake()
    {
        playerScore = 0;
    }


    /*
     * Updates player score and broadcasts to UI
     * int -> void
     */
    private void UpdateScore(int scoreValue)
    {
        playerScore += scoreValue;
        // Listens to ProjectileActions for missleHit, adjusts player score accordingly

        if (OnUpdatePlayerScore != null)
        {
            OnUpdatePlayerScore();
        }
    }


    /*
     * Updates player health value and broadcasts to UI
     * int -> void
     */
    private void UpdateHealthValue(int healthValue)
    {
        if (OnUpdatePlayerHealth != null)
        {
            OnUpdatePlayerHealth();
        }
    }

    private void OnEnable()
    {
        ProjectileActions.OnPlayerHit += UpdateHealthValue;
    }

    private void OnDisable()
    {
        ProjectileActions.OnPlayerHit -= UpdateHealthValue;
    }
}
