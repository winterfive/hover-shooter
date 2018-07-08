using UnityEngine;

public class GameManager : MonoBehaviour{
    
    public float timeBetweenShots = 0.15f;
    public RaycastManager raycastManager;
    public ShapeManager shapeManager;

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
            //Shoot();
        }
    }

    //  Destroys object
    //  void -> void
    //public void Shoot()
    //{
    //    _currentfoundobject = raycastmanager._currentfoundobject;
    //    shapemanager.destroyshape(_currentfoundobject);
    //    debug.log("shot an object: " + _currentfoundobject.name);
    //}
}
