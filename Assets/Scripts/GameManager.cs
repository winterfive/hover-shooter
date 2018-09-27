using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : GenericManager<GameManager>
{
    /*
     * Class handles all player input, score and 
     * gameplay actions (game over, start, restart, etc)
     */
     
    public float timeBetweenShots = 0.15f;
    public delegate void Shoot();
    public static event Shoot OnShoot;    
    public delegate void UpdateScore(int i);
    public static UpdateScore OnUpdateScore;
    public delegate void UpdatePlayerHealth(int j);
    public static UpdatePlayerHealth OnUpdatePlayerHealth;    

    private float _timeSinceLastShot;
    private bool _IsShieldUp;
    private int _score;
    private RaycastManager _raycastManager;
    private CheckForEnemy _checkForEnemy;
    private EffectsManager _effectsManager;


    void Awake()
    {
        _timeSinceLastShot = 0f;
        _IsShieldUp = false;
        _score = 0;
        _raycastManager = RaycastManager.Instance;
        _effectsManager = EffectsManager.Instance;
        _checkForEnemy = CheckForEnemy.Instance;
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
     * Broadcasts to PlayerLasers
     * void -> void
     */
    private void ShootAtEnemy()
    {
        if (OnShoot != null)
        {
            OnShoot();

            if (_checkForEnemy.IsEnemy())
            {
                DestroyEnemy();
            }
        }
    }


    private void DestroyEnemy()
    {
        //shotObject.GetComponent<NavMeshAgent>().speed = 0;
        //shotObject.GetComponent<DroneActions>().IsShooting = false;
        _effectsManager.DissolveEnemy();
        UpdateScore();
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
        if (OnUpdateScore != null)
        {
            OnUpdateScore(_score);
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
