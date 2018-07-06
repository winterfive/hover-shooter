using System;
using UnityEngine;

public class ReticleManager : MonoBehaviour {

    public GameObject reticle;
    public RaycastManager raycastManager;

    private GameObject _objectFound;
    private Vector3 _reticleNormalPosition;


	void Awake ()
    {
        // TODO _reticleNormalPosition = GetComponent camera position and rotation and add to it
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    // Places Reticle object on object found by raycast
    // GameObject -> void
    void PlaceReticle(GameObject objectFound)
    {
        //Debug.Log("Reticle thinks this is objectFound: " + objectFound);
    }

    void ChangeReticleColor()
    {
        
        //Debug.Log("Reticle thinks this is objectFound: " + _objectFound);

        // TODO Display reticle on foundObject (position, rotation)

        // TODO if(objectFound is Shootable), change color of reticle
    }
}
