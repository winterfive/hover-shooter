using UnityEngine;

public class GameManager : MonoBehaviour{
    
    public float timeBetweenShots = 0.15f;
    public DroneManager droneManager;

    private float _timer;    


    void Awake()
    {
        _timer = 0f;
    }


    private void Update()
    {
        _timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && _timer >= timeBetweenShots && Time.timeScale != 0)
        {
            droneManager.DestroyDrone();
        }
    }
}
