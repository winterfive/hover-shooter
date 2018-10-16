using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneShooting : MonoBehaviour {

    private MissleValues _DMVRef;
    private GameObject _gunTip;

    private void Awake()
    {
        GameObject missleValuesObject = GameObject.FindWithTag("ScriptManager");
        if (missleValuesObject != null)
        {
            _DMVRef = missleValuesObject.GetComponent<MissleValues>();
        }
        else
        {
            Debug.Log("Cannot find DroneMissleValues script");
        }
    }

    

    // Need call to shoot in update

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
