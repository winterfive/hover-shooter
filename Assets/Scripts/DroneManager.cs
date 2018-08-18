﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneManager : GenericManager<DroneManager>
{
    public Transform[] spawnpoints;
    public Transform[] endPoints;
    public GameObject dronePrefab;
    public int dronePoolSize;
    public float xMin, xMax, yMin, yMax, zMin, zMax;
    public float missleSpeed;
    
    [SerializeField] private float _timeBetweenSpawns;
    [SerializeField] private float _waitToSpawn;

    private Transform _camTransform;
    private List<GameObject> _drones;
    private List<GameObject> _missles;
    private PoolManager _poolManager;
    

    private void Awake()
    {
        _poolManager = PoolManager.Instance;
        _drones = _poolManager.CreateList(dronePrefab, dronePoolSize);
        _missles = _poolManager.CreateList(misslePrefab, misslePoolSize);
    }

    void Start()
    {
        _camTransform = Camera.main.gameObject.transform;
        InvokeRepeating("SpawnDrone", _waitToSpawn, _timeBetweenSpawns);
    }


    private void Update()
    {
        // TODO if missle reaches player collider, set active to false
    }


    /*
     * Spawns gameObject at random spawnpoint
     * void -> void
     */
    void SpawnDrone()
    {
        int spawnPointIndex;

        spawnPointIndex = Random.Range(0, spawnpoints.Length);

        GameObject drone = GetObjectFromPool(_drones);

        if (drone)
        {
            drone.transform.position = spawnpoints[spawnPointIndex].position;
            drone.transform.rotation = spawnpoints[spawnPointIndex].rotation;
            drone.SetActive(true);
        }
        else
        {
            Debug.Log("No more drones in list to use.");
        }               
    }


    private GameObject GetObjectFromPool(List<GameObject> l)
    {
        foreach (GameObject g in l)
        {
            if (!g.activeInHierarchy)
            {
                return g;
            }            
        }
        return null;
    }


    public void ReturnToPool(GameObject go)
    {
        go.SetActive(false);
    }


    /*
     * Creates Vector3 w/ random values for x & z w/in range
     * void -> Vector3
     */
    public Vector3 CreateRandomPosition()
    {
        Vector3 randomPosition;

        randomPosition.x = Random.Range(xMin, xMax);
        randomPosition.y = Random.Range(yMin, yMax);
        randomPosition.z = Random.Range(zMin, zMax);

        return randomPosition;
    }


    /*
     * Returns a random endPoint
     * void -> Vector3
     */
    public Vector3 SelectLastPosition()
    {
        int index = Random.Range(0, endPoints.Length);
        Vector3 endPosition = endPoints[index].position;
        return endPosition;
    }
}
