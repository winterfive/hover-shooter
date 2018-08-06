using UnityEngine;

public class GameManager : MonoBehaviour{
    
    public float timeBetweenShots = 0.15f;
    public delegate void ShotEnemy();
    public static event ShotEnemy OnShotEnemy;

    private float _timer;    


    void Awake()
    {
        _timer = 0f;
    }


    private void Update()
    {
        _timer += Time.deltaTime;
        Debug.Log("Got past timer variable");

        if (Input.GetButton("Jump") && _timer >= timeBetweenShots && Time.timeScale != 0)
        {
            Debug.Log("Got past space button");
            if (OnShotEnemy != null)
            {
                Debug.Log("Got to event call");
                OnShotEnemy();
            }
        }
    }
}