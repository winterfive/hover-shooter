using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneManager : MonoBehaviour {

    public Transform[] Spawnpoints;
    public GameObject drone;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] float waitToSpawn;

    void Start()
    {
        InvokeRepeating("SpawnDrones", waitToSpawn, timeBetweenSpawns);
    }

    /*
     * Spawns drone at random spawnpoint
     * void -> void
     */
    void SpawnDrones()
    {
        int spawnPointIndex = Random.Range(0, Spawnpoints.Length);
        Instantiate(drone, Spawnpoints[spawnPointIndex].position, Spawnpoints[spawnPointIndex].rotation); 
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


    void DroneShoot()
    {
        // If(drone can see player)
       
        // {  
        //      Shoot()  Use raycastManger to make a line?
        // }
    }


    void LookAtPlayer()
    {
        // TODO
    }    
}
