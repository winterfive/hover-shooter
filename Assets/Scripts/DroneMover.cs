using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//  Drone class can handle more then one model of drone
//  Each model will ahve it's own animations, colors, and effects

public class DroneManager : MonoBehaviour {

    public Transform[] Spawnpoints;
    public float spawnTime;
    public float waitToSpawn;

    private Vector3 camPosition;
    
    

    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        camPosition = Camera.main.gameObject.transform.position;
        agent.destination = camPosition;
    }

    void SpawnDrones()
    {
        // GetComponent random spawnpoint value
        // Get drone from object pool
        // Set shooting to true
        // "Spawn" drone
    }

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

    }
}
