using System;
using UnityEngine;

public class RaycastManager : MonoBehaviour {

    public float range = 100f;
    public delegate void NewObjectFound();
    public static event NewObjectFound OnNewObjectFound;
    public delegate void NewNormalFound();
    public static event NewNormalFound OnNewNormalFound;

    private RaycastHit _hit;
    private GameObject _currentFoundObject;
    private GameObject _previousFoundObject;
    private Vector3 _currentNormal;
    private Vector3 _previousNormal;

    public GameObject GetCurrentFoundObject() { return _currentFoundObject; }
    public GameObject GetPreviousFoundObject() { return _previousFoundObject; }
    public Vector3 GetCurrentNormal() { return _currentNormal; }
    public Vector3 GetPreviousNormal() { return _previousNormal; }
    public RaycastHit GetCurrentHit() { return _hit; }


    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out _hit, range))
        {
            CheckForNewObject(_hit);
            CheckForNewNormal(_hit);
        }        
    }

    //  Compares newly found object with previously found object
    //  Checks newly found object for "Drone" tag
    //  Calls event
    //  void -> void
    public void CheckForNewObject(RaycastHit hit)
    {
        GameObject newObject = hit.collider.gameObject;

        if (!newObject.Equals(_currentFoundObject))
        {
            if(newObject.tag == "Drone")
            {
                if (OnNewObjectFound != null)
                {
                    _previousFoundObject = _currentFoundObject;
                    _currentFoundObject = newObject;
                    OnNewObjectFound();
                }
            }            
        }
    }

    // Compares newly found normal with previously found normal
    // Calls event
    // RaycastHit -> void    
    public void CheckForNewNormal(RaycastHit hit)
    {
        Vector3 newNormal = hit.normal;

        if(!newNormal.Equals(_currentNormal))
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
