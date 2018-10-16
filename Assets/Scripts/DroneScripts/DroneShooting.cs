﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneShooting : DroneActions {

    private MissleValues _MVRef;
    private GameObject _this;
    private Transform _gunTipTransform;
    private float _timeBetweenMissles;
    private float _timeOfPreviousShot;
    private RaycastHit _hit;
    private int _droneRange;

    private void Awake()
    {
        GameObject missleValuesObject = GameObject.FindWithTag("ScriptManager");
        if (missleValuesObject != null)
        {
            _MVRef = missleValuesObject.GetComponent<MissleValues>();
        }
        else
        {
            Debug.Log("Cannot find DroneMissleValues script");
        }

        _this = this.gameObject;
        _gunTipTransform = FindChildWithTag("GunTip", _this);
        _timeBetweenMissles = Random.Range(_MVRef.minMFireRate, _MVRef.maxMFireRate);
        _droneRange = _MVRef.droneRange;
    }


    private void Update()
    {
        ShootMissles();
    }
    

    private void ShootMissles()
    {
        if (Time.time > _timeOfPreviousShot + _timeBetweenMissles)
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