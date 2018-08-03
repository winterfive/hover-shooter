using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneManager : MonoBehaviour {

    public Transform[] Spawnpoints;
    public RaycastManager raycastManager;
    public EffectsManager effectsManager;

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
    public void DestroyDrone()
    {
        GameObject enemyShot = raycastManager.GetCurrentFoundObject();
        Transform shotTransform = enemyShot.transform;

        // glow lerp gets fast for a second, then stops
        // Call explosion script in EffectsManager
        // stop shooting script, shooting = false
        // begin drone hit anim (drone wavers and tilts)
        // turn on gravity for drone
        // Call smoke script in EffectsManager
        // Drone falls through floor slowly

        enemyShot.SetActive(false);
    }
}
