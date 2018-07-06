using UnityEngine;

public class GameManager : MonoBehaviour{
    
    public float timeBetweenShots = 0.15f;
    public RaycastManager raycastManager;
    public ShapeManager shapeManager;

    private float _timer;    
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

        if (_timer >= timeBetweenShots && Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    //  Destroys object
    //  void -> void
    public void Shoot()
    {
        _currentFoundObject = raycastManager.GetCurrentFoundObject;
        shapeManager.DestroyShape(_currentFoundObject);
        Debug.Log("Shot an object: " + _currentFoundObject.name);
    }}
}
