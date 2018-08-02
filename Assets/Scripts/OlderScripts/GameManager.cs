using UnityEngine;

public class GameManager : MonoBehaviour{
    
    public float timeBetweenShots = 0.15f;
    public RaycastManager raycastManager;

    private float _timer;    


    // Use this for initialization
    void Awake()
    {
        _timer = 0f;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= timeBetweenShots && Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    //  Destroys object
    //  void -> void
    public void Shoot()
    {
        //shapeManager.DestroyShape();
    }
}
