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
        // Add check for if shield is up
        if (Input.GetButton("Fire1") && (Time.time >= timeBetweenShots + _timeSinceLastShot) && Time.timeScale != 0)
        {
            //OnShoot();

            _timeSinceLastShot = Time.time;
        }

        //if (player has shield up button pressed)
        //{
        //    if (shield has energy to use)
        //    {
        //        IsShieldUp = true;
        //        while (shield energy level > 0)
        //        {
        //            tell UIManager to display shield in HMD
        //            tell UI Manager to remove power level from shield for each second up
        //            also remove energy based on enemy projectile hits ?
        //        }
        //    }
        //    else
        //    {
        //        tell UIManager to let player know there's no energy for shield
        //    }
        //}
        //else
        //{
        //    IsShieldUp = false;
        //}
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
