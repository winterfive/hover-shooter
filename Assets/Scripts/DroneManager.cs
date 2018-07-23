using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//  Drone class can handle more then one model of drone
//  Each prefab/model will have it's own animations, colors, and effects

public class DroneManager : MonoBehaviour {

    public Transform[] Spawnpoints;
    public GameObject drone;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] float waitToSpawn;

    private void Awake()
    {

    }

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
        // stop color change in EffectManager
        // Call explosion script in EffectsManager
        // stop shooting script, shooting = false
        // begin drone failling anim (drone crashes, tiled on floor)
        // Call smoke script in EffectsManager
        // Drone falls through floor slowly
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

    }
}
