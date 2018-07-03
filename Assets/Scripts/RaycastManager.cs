using UnityEngine;

public class RaycastManager : MonoBehaviour {

    public float range = 100f;
    
    private RaycastHit hit;
    private GameObject _objectFound;
    private bool _hasHitObject;
    private bool _ifShootable;

    public bool HasHitObject
    {
        get { return _hasHitObject; }
    }

    public GameObject GetObjectFound
    {
        get { return _objectFound; }
    }

    public bool IfShootable
    {
        get { return _ifShootable; }
    }

    // Sends out ray
    // Updates bool value for _hasHitObject
    // void -> void
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            //Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.magenta); works
            _hasHitObject = true;
            StoreObject();
            CheckForShootable();
        }
        else
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * range, Color.blue); works
            _hasHitObject = false;           
        }        
    }

    //  Stores found object
    //  void -> void
    public void StoreObject()
    {
        if(HasHitObject)
        {
            _objectFound = hit.collider.gameObject;
            //Debug.Log("Object assigned to objectFound: " + hit.collider.tag); works
            //Debug.Log("hasHitObject: " + _hasHitObject.ToString()); works
        }
        else
        {
            _objectFound = null;
        }
    }

    // Checks object found for "Shootable" tag, updates bool ifShootable
    // void -> void
    public void CheckForShootable()
    {
        if(_objectFound.tag == "Shootable")
        {
            _ifShootable = true;
        }
        else
        {
            _ifShootable = false;
        }
    }
}
