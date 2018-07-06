using UnityEngine;

public class GameManager : MonoBehaviour{
    
    public float timeBetweenShots = 0.15f;
    public RaycastManager raycastManager;
    public ShapeManager shapeManager;

    private float _timer;
    private bool _isObjectShootable;    
    private GameObject _currentFoundObject;    


    // Use this for initialization
    void Awake()
    {
        _timer = 0f;
        _currentFoundObject = null;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_isObjectShootable && _timer >= timeBetweenShots && Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    //  Destroys object
    //  void -> void
    public void Shoot()
    {
        _currentFoundObject = raycastManager.GetObjectFound;
        shapeManager.DestroyShape(_currentFoundObject);
        Debug.Log("Shot an object: " + _currentFoundObject.name);
    }

    // Updates bool isObjectSHootable
    // void -> void
    void Shootable()
    {
        if (raycastManager.IfShootable)
        {
            _isObjectShootable = true;
        }
        else
        {
            _isObjectShootable = false;
        }
    }
}
