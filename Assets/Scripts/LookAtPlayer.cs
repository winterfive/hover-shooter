using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

    private Transform _camTransform;

    
    void Start ()
    {
        _camTransform = Camera.main.gameObject.transform;
    }

	
	void Update ()
    {
        PointTurretAtPlayer();		
	}


    /*
     * Rotates turret towards player on Y axis
     * void -> void
     */
    private void PointTurretAtPlayer()
    {
        if (this.isActiveAndEnabled)
        {
            Vector3 newVector = new Vector3(this.transform.position.x - _camTransform.position.x,
                                            0f,
                                            this.transform.position.z - _camTransform.position.z);

            this.transform.rotation = Quaternion.LookRotation(newVector);
        }
    }
}
