using System;
using UnityEngine;

public class Reticle : MonoBehaviour {

    private RaycastManager _raycastManager;
    private GameObject _objectFound;
    private MeshRenderer _meshRenderer;
    private Vector3 _reticleNormalPosition;


	void Awake ()
    {
        _raycastManager = null;
        _objectFound = null;
	}
	
	// Update is called once per frame
	void Update ()
    {    
        if (_raycastManager.HasHitObject)
        {
            _objectFound = _raycastManager.GetObjectFound;
            PlaceReticle();
        }
        else
        {
            // place reticle infront camera at a certain distance
        }
	}

    // Places Reticle object on object found by raycast
    // GameObject -> void
    void PlaceReticle()
    {

    }

    void ChangeReticleColor()
    {
        
        Debug.Log("Reticle thinks this is objectFound: " + _objectFound);

        // TODO Display reticle on foundObject (position, rotation)

        // TODO if(objectFound is Shootable), change color of reticle
    }

}
