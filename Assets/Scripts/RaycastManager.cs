using UnityEngine;

public class RaycastManager : MonoBehaviour {

    public float range = 100f;
    
    private RaycastHit hit;
    private GameObject _objectFound;
    private bool _ifShootable;

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
            StoreObject();
            CheckForShootable();
        }
        else
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * range, Color.blue); works           
        }        
    }

    //  Stores found object
    //  void -> void
    public void StoreObject()
    {
            _objectFound = hit.collider.gameObject;
            // TODO checks currentObjectFound against previosuObjectFound
            // TODO if different, send an event to shapeManager
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
