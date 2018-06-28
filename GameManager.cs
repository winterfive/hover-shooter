using UnityEngine;

public class GameManager : MonoBehaviour{
    
    public float timeBetweenShots = 0.15f;

    private float _timer;
    private bool _isObjectShootable;
    private RaycastManager _raycastManager;
    private GameObject _currentFoundObject;
    private ShapeManager _shapeManager;


    // Use this for initialization
    void Awake()
    {
        _timer = 0f;
        _raycastManager = null;
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
        _shapeManager.DestroyShape(_currentFoundObject);
        Debug.Log("Shot an object");
    }

    // Updates bool isObjectSHootable
    // void -> void
    void Shootable()
    {
        if (_raycastManager.IfShootable)
        {
            _isObjectShootable = true;
        }
        else
        {
            _isObjectShootable = false;
        }
    }
}
