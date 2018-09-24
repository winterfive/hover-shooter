using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDroneManager : DroneManager {

    /*
     * This class handles values & methods needed for spawning and moving attackDrones
     */

    public Transform[] spawnPoints;
    public Transform[] endPoints;
    public GameObject prefab;
    public int poolSize;
    public float xMin, xMax, yMin, yMax, zMin, zMax;
    public float timeBetweenSpawns;
    public float waitToSpawn;

    private PoolManager _poolManager;
    private List<GameObject> _attackDrones;
    private GameObject _activeAttackDrone;
    private PlayerManager _playerManager;


    private void Awake()
    {
        _poolManager = PoolManager.Instance;
        _playerManager = PlayerManager.Instance;    // Should these references go to the parent class?
        _attackDrones = _poolManager.CreateList(prefab, poolSize);
    }


    void Start()
    {
        StartCoroutine(SpawnAttackDrone());
    }


    private IEnumerator SpawnAttackDrone()
    {
        while (_playerManager.IsAlive())
        {
            _activeAttackDrone = _poolManager.GetObjectFromPool(_attackDrones);

            if (_activeAttackDrone)
            {
                Transform startPoint = GetRandomValueFromArray(spawnPoints);
                _activeAttackDrone.transform.position = startPoint.position;
                _activeAttackDrone.transform.rotation = startPoint.rotation;
                _activeAttackDrone.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("There aren't any attackDrones available right now.");
            }
        }
        yield return new WaitForSeconds(timeBetweenSpawns);
    }


    public Vector3 CreateRandomDestination()
    {
        Vector3 newPoint = CreateRandomVector(xMin, xMax, yMin, yMax, zMin, zMax);
        return newPoint;
    }


    public Vector3 SetEndPoint()
    {
        int point = Random.Range(0, endPoints.Length);
        Vector3 newPoint = endPoints[point].position;
        return newPoint;
    }
}