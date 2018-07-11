using System;
using UnityEngine;

public class RaycastManager : MonoBehaviour {

    public float range = 100f;
    public delegate void NewObjectFound();
    public static event NewObjectFound OnNewObjectFound;
    public delegate void NewNormalFound();
    public static event NewNormalFound OnNewNormalFound;

    private GameObject _currentFoundObject;
    private GameObject _previousFoundObject;
    private RaycastHit _currentNormal;
    private RaycastHit _previousNormal;

    public GameObject GetCurrentFoundObject()
    {
        return _currentFoundObject;
    }

    public GameObject GetPreviousFoundObject()
    {
        return _previousFoundObject;
    }

    public RaycastHit GetCurrentHit()
    {
        return _currentNormal;
    }

    public RaycastHit GetPreviousHit()
    {
        return _previousNormal;
    }


    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out _currentNormal, range))
        {
            CheckForNewObject();
            CheckNormal();
        }        
    }

    //  Compares newly found object with previously found object
    //  Calls event
    //  void -> void
    public void CheckForNewObject()
    {
        GameObject foundObject = _currentNormal.collider.gameObject; 

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
    // void -> void    
    public void CheckNormal()
    {
        RaycastHit newHit = GetCurrentHit();

        if(!newHit.Equals(_previousNormal))
        {
            if (OnNewNormalFound != null)
            {
                _previousNormal = _currentNormal;
                _currentNormal = newHit;
                OnNewNormalFound();
            }
        }
    }

    internal RaycastHit GetRaycastHit()
    {
        throw new NotImplementedException();
    }
}
