using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneManager : MonoBehaviour {

    public Transform[] Spawnpoints;

    [SerializeField] float timeBetweenSpawns;
    [SerializeField] float waitToSpawn;

    void Start()
    {
        InvokeRepeating("SpawnDrones", waitToSpawn, timeBetweenSpawns);
    }


    /*
     * Gets drone from object pool, spawns it at random spawnpoint
     * void -> void
     */
    void SpawnDrones()
    {
        int spawnPointIndex;
        
        spawnPointIndex = Random.Range(0, Spawnpoints.Length);

        GameObject drone = NewObjectPooler.currentPooler.GetPooledObject();

        if (drone == null) return;

        drone.transform.position = Spawnpoints[spawnPointIndex].position;
        drone.transform.rotation = Spawnpoints[spawnPointIndex].rotation;
        drone.SetActive(true);
    }


    /*
     * Handles all actions required when drone is shot by player
     * void -> void
     */
    void DestroyDrone()
    {
        // listens for event
        // stop colorLerp in DroneMover
        // Call explosion script in EffectsManager
        // start colorBlink corountine in DroneMover
        // stop shooting script, shooting = false
        // begin drone hit anim (drone wavers and tilts)
        // turn on gravity for drone
        // Call smoke script in EffectsManager
        // Drone falls through floor slowly
        // Stop colorBlink coroutine
        // Requeue drone object
    }    
}
