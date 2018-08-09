using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneManager : MonoBehaviour
{
    public Transform[] spawnpoints;
    public Transform[] endPoints;
    public PoolManager poolManager;
    public GameObject prefab;
    public int poolSize;
    public float xMin, xMax, yMin, yMax, zMin, zMax;    

    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private float waitToSpawn;

    private List<GameObject> drones;


    private void Awake()
    {
        drones = poolManager.CreateList(prefab, poolSize);
    }

    void Start()
    {
        InvokeRepeating("SpawnEnemy", waitToSpawn, timeBetweenSpawns);
    }


    /*
     * Spawns gameObject at random spawnpoint
     * void -> void
     */
    void SpawnEnemy()
    {
        int spawnPointIndex;

        spawnPointIndex = Random.Range(0, spawnpoints.Length);

        GameObject drone = GetObjectFromPool();

        if (drone != null)
        {
            drone.transform.position = spawnpoints[spawnPointIndex].position;
            drone.transform.rotation = spawnpoints[spawnPointIndex].rotation;
            drone.SetActive(true);
        }
        else
        {
            Debug.Log("No more drones in list to use.");
        }               
    }


    public void ReturnToPool(GameObject go)
    {
        Debug.Log("Drone returned to pool");
        go.SetActive(false);
    }


    GameObject GetObjectFromPool()
    {
        foreach (GameObject drone in drones)
        {
            if (!drone.activeInHierarchy)
            {
                return drone;
            }            
        }
        return null;
    }


    /*
     * Creates Vector3 w/ random values for x & z w/in range
     * void -> Vector3
     */
    public Vector3 CreateRandomPosition()
    {
        Vector3 randomPosition;

        randomPosition.x = Random.Range(xMin, xMax);
        randomPosition.y = Random.Range(yMin, yMax);
        randomPosition.z = Random.Range(zMin, zMax);

        return randomPosition;
    }


    /*
     * Changes agent direction to random end point
     * void -> Vector3
     */
    public Vector3 GetEndPosition()
    {
        int index = Random.Range(0, endPoints.Length);
        Vector3 endPosition = endPoints[index].position;
        return endPosition;
    }
}
