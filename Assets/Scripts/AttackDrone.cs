using System;
using UnityEditor;

public class AttackDrone : Drone, MonoBehaviour
{
    private _attackDroneManagerReference;

    void Awake()
    {
        _poolManager = Drone.PoolManager.Instance;
        _drones = _poolManager.CreateList(dronePrefab, dronePoolSize);
    }

    void Start()
    {
        GameObject attackDroneManagerObject = GameObject.FindWithTag("ScriptManager");
        if (attackDroneManagerObject != null)
        {
            _attackDroneManagerReference = attackDroneManagerObject.GetComponent<DroneManager>();
        }

        if (_droneManagerReference == null)
        {
            Debug.Log("Cannot find DroneManager script");
        }
    }
}
