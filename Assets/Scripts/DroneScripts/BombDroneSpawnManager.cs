using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Handles Bomb Drone spawning
 * Bomb Drone is unique
 */
public class BombDroneSpawnManager : PoolingManager {

    private PlayerManager _playerManager;
    private BombDroneValues _BDV;
    private List<GameObject> _bombDrones;
    private GameObject _activeBombDrone;
    private DroneActions _droneActions;
    private WaitForSeconds _waitBetweenSpawns;


    private void Awake()
    {
        _BDV = BombDroneValues.Instance;
        _playerManager = PlayerManager.Instance;
        _droneActions = DroneActions.Instance;
        _bombDrones = CreateList(_BDV.prefab, _BDV.poolSize);
        _waitBetweenSpawns = new WaitForSeconds(_BDV.timeBetweenSpawns);
    }


    void Start()
    {
        StartCoroutine(SpawnBombDrone());
    }


    /*
     * Spawns bomb drone at one of the spawnPoints
     * void -> void
     */
    private IEnumerator SpawnBombDrone()
    {
        while (_playerManager.IsAlive())
        {
            yield return _waitBetweenSpawns;
            _activeBombDrone = GetObjectFromPool(_bombDrones);

            if (_activeBombDrone)
            {
                _droneActions.SetAtRandomPoint(_BDV.spawnPoints, _activeBombDrone);
                _activeBombDrone.SetActive(true);
            }
            else
            {
                Debug.Log("Bomb Drone is busy right now.");
            }
        }        

        yield return null;
    }
}
