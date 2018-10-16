using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : SetAsSingleton<GameManager>
{
    /*
     * Class handles all player input, score and 
     * gameplay actions (game over, start, restart, etc)
     */
     
    public float timeBetweenShots = 0.15f;
    public delegate void Shoot();
    public static event Shoot OnShoot;    
    public delegate void UpdateScore(int j);
    public static UpdateScore OnUpdateScore;
    public delegate void UpdatePlayerHealth(int i);
    public static UpdatePlayerHealth OnUpdatePlayerHealth;    

    private float _timeSinceLastShot;
    private bool _IsShieldUp;
    private int _score;
    private CheckForEnemy _checkForEnemy;
    private int _changeToHealthValue;
    private int _changeToScoreValue;
    private EffectsManager _effectsManager;


    void Awake()
    {
        _timeSinceLastShot = 0f;
        _IsShieldUp = false;
        _score = 0;
        _checkForEnemy = CheckForEnemy.Instance;
        _effectsManager = EffectsManager.Instance;
    }


    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && (Time.time >= timeBetweenShots + _timeSinceLastShot) && Time.timeScale != 0 && !_IsShieldUp)
        {
            ShootAtEnemy();

            _timeSinceLastShot = Time.time;
        }

        //if (Input.GetButton("Shield") && !Input.GetButtonDown("Fire1"))
        //{
        //    UseShield();
        //}

        //if (player is shot)
        //{
        //    take away player health
        //}
    }


    /*
     * Broadcasts to PlayerLasers and checks if object fired on
     * is an enemy
     * void -> void
     */
    private void ShootAtEnemy()
    {
        if (OnShoot != null)
        {
            OnShoot();

            if (_checkForEnemy.IsEnemy())
            {
                ShootDrone();
            }
        }
    }


    private void ShootDrone()
    {
        _effectsManager.EnemyDisapears();
        UpdatePlayerScore();
    }
    
      

    /*
     * Handles use of shield
     * void -> void
     */
    private void UseShield()
    {
    //    if (shieldPower > 0)
    //    {
    //        _IsShieldUp = true;

    //        while (shieldPower > 0)
    //        {            
    //            display shield
    //            remove power from shield per each second in use in gameManager
    //            tell UI Manager to remove power level from shield for each second up
    //            also remove energy based on enemy projectile hits ?
    //        }
    //    }
    //    else
    //    {
    //        tell UIManager to let player know shield has no power
    //        _IsShieldUp = false;
    //    }        
    }


    /*
     * Broadcasts change in player score to UI
     * void -> void
     */
    private void UpdatePlayerScore()
    {
        if (OnUpdateScore != null)
        {
            OnUpdateScore(_changeToScoreValue);
        }
    }


    /*
     * Broadcasts change in player health to UI
     * void -> void
     */
    private void UpdateHealth()
    {
        if (OnUpdatePlayerHealth != null)
        {
            OnUpdatePlayerHealth(_changeToHealthValue);
        }
    }


    private void OnEnable()
    {
        MissleActions.OnPlayerHit += UpdateHealth;
    }

    private void OnDisable()
    {
        MissleActions.OnPlayerHit -= UpdateHealth;
    }
}
