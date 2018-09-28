using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDroneManager : PooledObjectManager {

    /*
     * This class handles values & methods needed for spawning and moving bombDrones
     * BombDrones do not shoot at the player.  They flaot slowly to the player and once within
     * a certain range, detonate, inflicting a large amount of damage to the player.
     */

    public Transform[] spawnPoints;
    public GameObject prefab;
    public int poolSize;
    public float minSpeed, maxSpeed;
    public float altitudeMin, altitudeMax;
    public float waitToSpawn, timeBetweenSpawns;
    public float glowSpeed;
    public Color secondGlowColor;
    public int damageValue, pointValue;
    
    private PoolManager _poolManager;
    private List<GameObject> _bombDrones;
    private GameObject _activeBombDrone;
    private PlayerManager _playerManager;
    private bool _bombDroneAlive;
    private Transform _camTransform;


    private void Awake()
    {
        _poolManager = PoolManager.Instance;
        _playerManager = PlayerManager.Instance;
        _bombDrones = _poolManager.CreateList(prefab, poolSize);
        _bombDroneAlive = false;
        _camTransform = Camera.main.transform;
    }


    void Start()
    {
        if (!_bombDroneAlive)
        {
            SpawnBombDrone();
        }        
    }


    /*
     * Spawns bomb drone at one of five starting points
     * void -> void
     */
    private void SpawnBombDrone()
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


    public Vector3 SetEndPoint()
    {
        Vector3 end = _camTransform.position + new Vector3(0f, 1f, 1f);
        return end;
    }


    public float GetRandomSpeed()
    {
        float value = Random.Range(minSpeed, maxSpeed);
        return value;
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