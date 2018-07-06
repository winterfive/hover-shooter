using UnityEngine;

public class RaycastManager : MonoBehaviour {

    public float range = 100f;
    public static bool _hasNewObject;

    private RaycastHit hit;
    private GameObject _currentFoundObject;
    private GameObject _previousFoundObject;

    public GameObject GetCurrentFoundObject()
    {
        return _currentFoundObject;
    }
    

    private void Start()
    {
        _hasNewObject = false;
    }


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
        _currentFoundObject = hit.collider.gameObject;

        if(!_currentFoundObject.Equals(_previousFoundObject))
        {
            _hasNewObject = true;
        }
        else
        {
            _hasNewObject = false;   
        }

        _previousFoundObject = _currentFoundObject;
    }    
}
