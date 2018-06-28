using System;
using UnityEngine;

public class ReticleManager : MonoBehaviour {

    public GameObject reticle;

    private RaycastManager _raycastManager;
    private GameObject _objectFound;
    private MeshRenderer _meshRenderer;
    private Vector3 _reticleNormalPosition;


	void Awake ()
    {
        _raycastManager = null;
        _objectFound = null;
        // TODO _reticleNormalPosition = GetComponent camera position and rotation and add to it
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_raycastManager.HasHitObject)
        {
            _objectFound = _raycastManager.GetObjectFound;
            //PlaceReticle(_objectFound);
        }
        else
        {
            // place reticle infront camera at default distance
        }
	}

    // Places Reticle object on object found by raycast
    // GameObject -> void
    void PlaceReticle()
    {
        Debug.Log("Reticle thinks this is objectFound: " + _objectFound);
    }

    void ChangeReticleColor()
    {
        
        //Debug.Log("Reticle thinks this is objectFound: " + _objectFound);

        // TODO Display reticle on foundObject (position, rotation)

        // TODO if(objectFound is Shootable), change color of reticle
    }

}
