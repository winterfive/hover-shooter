using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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
    public float destroyDroneDuration;
    public Color[] destructionColors;
    public float waitBetweenColors;

    private float _timeSinceLastShot;
    private bool _IsShieldUp;
    private int _score, _playerHealth;
    private RaycastManager _raycastManager;
    private GameObject shotObject;


    void Awake()
    {
        _timeSinceLastShot = 0f;
        _IsShieldUp = false;
        _score = 0;
        _playerHealth = 100;
        _raycastManager = RaycastManager.Instance;
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
    }


    /*
     * Broadcasts to PlayerLasers, checks object shot at and destroys it
     * void -> void
     */
    private void ShootAtEnemy()
    {
        OnShoot();
        CheckForEnemy();        
    }


    private void CheckForEnemy()
    {
        if (_raycastManager.GetCurrentFoundObject() != null)
        {
            shotObject = _raycastManager.GetCurrentFoundObject().transform.root.gameObject;

            if (shotObject.tag == "Enemy")
            {
                DestroyEnemy();
            }
        }        
    }


    private void DestroyEnemy()
    {
        shotObject.GetComponent<NavMeshAgent>().speed = 0;
        shotObject.GetComponent<DroneActions>().IsShooting = false;
        StartCoroutine("FadeEffect");
        shotObject.SetActive(false);
    }


    private IEnumerator FadeEffect()
    {
        Renderer[] components = shotObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer r in components)
        {
            r.material.mainTexture = null;
        }

        foreach (Color c in destructionColors)
        {
            foreach (Renderer r in components)
            {
                r.material.color = c;
            }

            yield return new WaitForSeconds(waitBetweenColors);
        }

        yield return new WaitForSeconds(destroyDroneDuration);
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
