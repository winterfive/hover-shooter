using System;
using UnityEngine;

public class RaycastManager : MonoBehaviour {

    public float range = 100f;
    public delegate void NewObjectFound();
    public static event NewObjectFound OnNewObjectFound;
    public delegate void NewNormalFound();
    public static event NewNormalFound OnNewNormalFound;

    private RaycastHit hit;
    private GameObject _currentFoundObject;
    private GameObject _previousFoundObject;
    private Vector3 _currentNormal;
    private Vector3 _previousNormal;

    public GameObject GetCurrentFoundObject()
    {
        return _currentFoundObject;
    }

    public GameObject GetPreviousFoundObject()
    {
        return _previousFoundObject;
    }

    public Vector3 GetCurrentNormal()
    {
        return _currentNormal;
    }

    public Vector3 GetPreviousNormal()
    {
        return _previousNormal;
    }


    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            CheckForNewObject();
            CheckNormal(hit);
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

    // Compares newly found normal with previously found normal
    // Calls event
    // RaycastHit -> void    
    public void CheckNormal(RaycastHit hit)
    {
        Vector3 newNormal = hit.normal;

        if(!newNormal.Equals(_previousNormal))
        {
            if (OnNewNormalFound != null)
            {
                _previousNormal = _currentNormal;
                _currentNormal = newNormal;
                OnNewNormalFound();
            }
        }
    }
}
