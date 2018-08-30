using System;
using UnityEngine;

public class GameManager : GenericManager<GameManager>
{
    /*
     * Class handles all player values (score, health, shield), player actions, and 
     * gameplay actions (game over, start, restart, etc)
     */
     
    public float timeBetweenShots = 0.15f;
    public delegate void Shoot();
    public static event Shoot OnShoot;    
    public delegate void UpdatePlayerScore(int i);
    public static UpdatePlayerScore OnUpdatePlayerScore;
    public delegate void UpdatePlayerHealth(int j);
    public static UpdatePlayerHealth OnUpdatePlayerHealth;
    public int missleHitValue;

    private float _timeSinceLastShot;
    private bool _IsShieldUp;
    private int _score, _playerHealth;


    void Awake()
    {
        _timeSinceLastShot = 0f;
        _IsShieldUp = false;
        _score = 0;
        _playerHealth = 100;
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
    }


    /*
     * Broadcasts to PlayerLasers and checks object shot at
     * void -> void
     */
    private void ShootAtEnemy()
    {
        OnShoot();

        //if (shot hits enemy)
        //{
        //    call destroy enemy
        //}
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
     * Updates player score and broadcasts to UI
     * void -> void
     */
    private void UpdateScore()
    {
        if (OnUpdatePlayerScore != null)
        {
            OnUpdatePlayerScore(_score);
        }
    }


    /*
     * Updates player health value and broadcasts to UI
     * void -> void
     */
    private void UpdateHealth()
    {
        _playerHealth -= 1;

        if (OnUpdatePlayerHealth != null)
        {
            OnUpdatePlayerHealth(_playerHealth);
        }
    }


    private void OnEnable()
    {
        ProjectileActions.OnPlayerHit += UpdateHealth;
    }

    private void OnDisable()
    {
        ProjectileActions.OnPlayerHit -= UpdateHealth;
    }
}
