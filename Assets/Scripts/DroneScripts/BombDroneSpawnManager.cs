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


    private void Awake()
    {
        _BDV = BombDroneValues.Instance;
        _playerManager = PlayerManager.Instance;
        _droneActions = DroneActions.Instance;
        _bombDrones = CreateList(_BDV.prefab, _BDV.poolSize);
    }


    void Update()
    {
        if (Time.time % 30 == 0)
        {
            if (_activeBombDrone == null || !_activeBombDrone.activeInHierarchy)
            {
                SpawnBombDrone();
            }
        }        
    }


    /*
     * Spawns attack drones at one of the spawnPoints
     * void -> void
     */
    private void SpawnBombDrone()
    {
        //if (_playerManager.isalive())
        //{
        //    _activebombdrone = getobjectfrompool(_bombdrones);

        //    if (_activebombdrone)
        //    {
        //        _droneactions.setatstart(_bdv.spawnpoints, _activebombdrone);
        //        _activebombdrone.setactive(true);
        //    }
        //    else
        //    {
        //        debug.log("The bomb drone is already active.");
        //    }

        //    startcoroutine(waitbetweenbombdrones());
        //}
    }


    private IEnumerator WaitBetweenBombDrones()
    {
        yield return new WaitForSeconds(_BDV.timeBetweenSpawns);
    }
}
