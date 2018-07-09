using UnityEngine;

public class RaycastManager : MonoBehaviour {

    public float range = 100f;
    public delegate void NewObjectFound();
    public static event NewObjectFound OnNewObjectFound;

    private RaycastHit hit;
    private GameObject _currentFoundObject;
    private GameObject _previousFoundObject;

    public GameObject GetCurrentFoundObject()
    {
        return _currentFoundObject;
    }

    public GameObject GetPreviousFoundObject()
    {
        return _previousFoundObject;
    }


    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            CheckForNewObject();
        }        
    }

    //  Compares newly found object with previously found object
    //  Calls event
    //  void -> void
    public void CheckForNewObject()
    {
        GameObject foundObject = hit.collider.gameObject; 

        if (!foundObject.Equals(_currentFoundObject))
        {
            if (OnNewObjectFound != null)
            {
                _previousFoundObject = _currentFoundObject;
                _currentFoundObject = foundObject;
                OnNewObjectFound();
            }
        }
    }    
}
