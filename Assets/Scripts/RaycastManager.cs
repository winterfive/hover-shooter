using UnityEngine;

public class RaycastManager : MonoBehaviour {

    public float range = 100f;
    
    private RaycastHit hit;
    private GameObject _currentObjectFound;
    private GameObject _previousFoundObject;

    public GameObject GetCurrentFoundObject
    {
        get { return _currentObjectFound; }
    }

    // Sends out ray
    // void -> void
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            //Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.magenta); works
            CheckForNewObject();
        }        
    }

    //  Stores found object
    //  Compares newly found object with previously found object
    //  void -> void
    public void CheckForNewObject()
    {
        _currentObjectFound = hit.collider.gameObject;

        if(_previousFoundObject != null)
        {
            if(!_currentObjectFound.Equals(_previousFoundObject))
            {
                // send event to ShapeManager
            }
        }

        _previousFoundObject = _currentObjectFound;
    }    
}
