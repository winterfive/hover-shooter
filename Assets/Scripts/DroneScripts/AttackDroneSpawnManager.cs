using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Handles Attack Drone spawning
 */
public class AttackDroneSpawnManager : PoolingManager {

    private PlayerManager _playerManager;
    private AttackDroneValues _ADV;
    private List<GameObject> _attackDrones;
    private GameObject _activeAttackDrone;
    private DroneActions _droneActions;


    private void Awake()
    {
        _ADV = AttackDroneValues.Instance;
        _playerManager = PlayerManager.Instance;
        _droneActions = DroneActions.Instance;
        _attackDrones = CreateList(_ADV.prefab, _ADV.poolSize);
    }


    void Start()
    {
        StartCoroutine(SpawnAttackDrone());
    }


    /*
     * Spawns attack drones at one of the spawnPoints
     * void -> void
     */
    private IEnumerator SpawnAttackDrone()
    {
        while (_playerManager.IsAlive())
        {
            _activeAttackDrone = GetObjectFromPool(_attackDrones);

            if (_activeAttackDrone)
            {
                _droneActions.SetAtRandomPoint(_ADV.spawnPoints, _activeAttackDrone);
                _activeAttackDrone.SetActive(true);
            }
            else
            {
                Debug.Log("There aren't any attackDrones available right now.");
            }

            yield return new WaitForSeconds(_ADV.timeBetweenSpawns);
        }
    }
}
