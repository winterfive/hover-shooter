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
            //Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.magenta);
            //Debug.Log("Did Hit: " + hit.collider.tag);
            _hasHitObject = true;
            StoreObject();
        }
        else
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * range, Color.blue);
            //Debug.Log("Did not Hit");
            _hasHitObject = false;           
        }        
    }

    //  Stores found object
    //  void -> void
    public void StoreObject()
    {
        if(HasHitObject)
        {
            _objectFound = hit.collider.GetComponent<GameObject>();
            Debug.Log("Object assigned to objectFound: " + hit.collider.tag);
        }
        else
        {
            _objectFound = null;
        }
    }

    public void CheckObject()
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
