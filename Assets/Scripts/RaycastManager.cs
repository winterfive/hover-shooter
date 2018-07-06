using UnityEngine;

public class RaycastManager : MonoBehaviour {

    public float range = 100f;
    
    private RaycastHit hit;
    private GameObject _objectFound;
    private GameObject _previousObjectFound;

    public GameObject GetObjectFound
    {
        get { return _objectFound; }
    }

    // Sends out ray
    // Updates bool value for _hasHitObject
    // void -> void
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            CheckForNewObject();
        }        
    }

    //  Stores found object adn compares it to previosu object
    //  void -> void
    public void CheckForNewObject()
    {
        _objectFound = hit.collider.gameObject;

        if(_previousObjectFound != null)
        {
            if (!_objectFound.Equals(_previousObjectFound))
            {
                //send an event to shapeManager, newObjectFound
            }
        }        

        _previousObjectFound = _objectFound;
    }    
}
