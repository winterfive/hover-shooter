using UnityEngine;

public class RaycastManager : SetAsSingleton<RaycastManager> {
    // Manages raycasting from the player's POV

    public float range = 100f;

    public delegate void NewObjectFound();
    public static event NewObjectFound OnNewObjectFound;
    public delegate void NewNormalFound();
    public static event NewNormalFound OnNewNormalFound;
    public delegate void NoObjectFound();
    public static event NoObjectFound OnNoObjectFound;

    private RaycastHit _hit;
    private GameObject _currentFoundObject;
    //private GameObject _previousFoundObject;
    private Vector3 _currentNormal;
    //private Vector3 _previousNormal;

    public GameObject GetCurrentFoundObject() { return _currentFoundObject; }
    //public GameObject GetPreviousFoundObject() { return _previousFoundObject; }
    public Vector3 GetCurrentNormal() { return _currentNormal; }
    //public Vector3 GetPreviousNormal() { return _previousNormal; }
    public RaycastHit GetCurrentHit() { return _hit; }


    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out _hit, range))
        {
            CheckForNewObject(_hit);
            CheckForNewNormal(_hit);
        }
        else
        {
            _currentFoundObject = null;
            if (OnNoObjectFound != null)
            {
                OnNoObjectFound();
            }
        }
    }

    //  Compares newly found object with previous object
    //  Calls event
    //  RaycastHit -> void
    public void CheckForNewObject(RaycastHit hit)
    {
        GameObject newObject = hit.collider.gameObject;

        if (!newObject.Equals(_currentFoundObject))
        {
            //_previousFoundObject = _currentFoundObject;
            _currentFoundObject = newObject;

            if (OnNewObjectFound != null)
            {                
                OnNewObjectFound();
            }
        }        
    }

    // Compares newly found normal with previously found normal
    // Calls event
    // RaycastHit -> void    
    public void CheckForNewNormal(RaycastHit hit)
    {
        Vector3 newNormal = hit.normal;

        if (!newNormal.Equals(_currentNormal))
        {
            //_previousNormal = _currentNormal;
            _currentNormal = newNormal;

            if (OnNewNormalFound != null)
            {
                OnNewNormalFound();
            }
        }
    }
}
