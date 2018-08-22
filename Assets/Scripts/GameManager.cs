using UnityEngine;

public class GameManager : GenericManager<GameManager>
{
    public float timeBetweenShots = 0.15f;
    public delegate void Shoot();
    public static event Shoot OnShoot;

    private float _timer, _timeSinceLastShot;


    void Awake()
    {
        _timer = 0f;
        _timeSinceLastShot = 0f;
    }


    private void Update()
    {
        _timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && (_timer >= timeBetweenShots + _timeSinceLastShot) && Time.timeScale != 0)
        {
            OnShoot();

            _timeSinceLastShot = _timer;
        }
    }
}
