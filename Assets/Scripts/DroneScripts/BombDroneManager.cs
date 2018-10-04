﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDroneManager : PoolingManager {

    /*
     * This class handles values & methods needed for spawning and moving bombDrones
     * BombDrones do not shoot at the player.  They move slowly to the player and once within
     * a certain range, detonate, inflicting a large amount of damage to the player.
     */

    public Transform[] spawnPoints;
    public GameObject prefab;
    public int poolSize;
    public float minSpeed, maxSpeed;
    public float altitudeMin, altitudeMax;
    public float xMin, xMax, yMin, yMax, zMin, zMax;
    public float glowSpeed;
    public Color secondGlowColor;
    public int damageValue, pointValue;
    
    private PoolManager _poolManager;
    private List<GameObject> _bombDrones;
    private GameObject _activeBombDrone;
    private PlayerManager _playerManager;
    private Transform _camTransform;
    //private bool _uniqueIsActive;


    private void Awake()
    {
        _poolManager = PoolManager.Instance;
        _playerManager = PlayerManager.Instance;
        _bombDrones = _poolManager.CreateList(prefab, poolSize);
        _camTransform = Camera.main.transform;
        _activeBombDrone = null;
    }


    void Update()
    {
        if (_activeBombDrone == null || !_activeBombDrone.activeInHierarchy)
        {
            SpawnBombDrone();
        }        
    }


    /*
     * Spawns bomb drone at one of five starting points
     * void -> void
     */
    private void SpawnBombDrone()
        //TODO Issue with spawn, its spawning and then dissapearing and spawning again elsewhere/ blinking
    {
        if (_playerManager.IsAlive())
        {
            _activeBombDrone = _poolManager.GetObjectFromPool(_bombDrones);

            if (_activeBombDrone)
            {
                Transform startPoint = GetRandomValueFromArray(spawnPoints);
                _activeBombDrone.transform.position = startPoint.position;
                _activeBombDrone.transform.rotation = startPoint.rotation;
                _activeBombDrone.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("The bombDrone isn't available right now.");
            }
        }            
    }


    public Vector3 SetMidPoint()
    {
        Vector3 mid = CreateRandomVector(xMin, xMax, yMin, yMax, zMin, zMax);
        return mid;
    }


    public Vector3 SetEndPoint()
    {
        Vector3 end = _camTransform.position;
        return end;
    }


    public float GetRandomSpeed()
    {
        float s = Random.Range(minSpeed, maxSpeed);
        return s;
    }


    public float GetRandomOffset()
    {
        float o = Random.Range(altitudeMin, altitudeMax);
        return o;
    }


    public void SetToInactive(GameObject go)
    {
        ReturnToPool(go);
    }
}