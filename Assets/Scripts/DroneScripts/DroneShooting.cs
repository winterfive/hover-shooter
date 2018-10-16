using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneShooting : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ShootMissles()
    {
        if (Time.time > _timeOfPreviousShot + _timeBetweenShots)
        {
            if (Physics.Raycast(_gunTipTransform.position, -_gunTipTransform.forward, out _hit, droneRange))
            {
                if (_hit.transform.tag == "Player")
                {
                    if (OnMissleFired != null)
                    {
                        OnMissleFired(_gunTipTransform);
                        _timeOfPreviousShot = Time.time;
                    }
                }
            }
        }
    }
}
