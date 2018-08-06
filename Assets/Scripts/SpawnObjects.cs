using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnObjects : MonoBehaviour {

    public Transform[] Spawnpoints;

    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private float waitToSpawn;

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
}
