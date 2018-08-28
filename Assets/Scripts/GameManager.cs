using UnityEngine;

public class GameManager : GenericManager<GameManager>
{
    public float timeBetweenShots = 0.15f;
    public delegate void Shoot();
    public static event Shoot OnShoot;

    private float _timeSinceLastShot;


    void Awake()
    {
        _timer = 0f;
        _timeSinceLastShot = 0f;
    }


    private void Update()
    {
        if (Input.GetButton("Fire1") && (Time.time >= timeBetweenShots + _timeSinceLastShot) && Time.timeScale != 0)
        {
            //OnShoot();

            _timeSinceLastShot = Time.time;
        }

        //if (player has shield up button pressed)
        //{
        //    if (shield has energy to use)
        //    {
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
    }    
}
