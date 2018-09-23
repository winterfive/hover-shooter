using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDroneManager : DroneManager {

    /*
     * This class handles values needed for spawning and moving attackDrones
     */

    public Transform[] spawnPoints;
    public Transform[] endPoints;
    public GameObject prefab;
    public int poolSize;
    public float xMin, xMax, yMin, yMax, zMin, zMax;

    [SerializeField] private float _timeBetweenSpawns;
    [SerializeField] private float _waitToSpawn;

    private PoolManager _poolManager;
    private List<GameObject> _attackDrones;
    private GameObject _activeAttackDrone;


    private void Awake()
    {
        _poolManager = PoolManager.Instance;
        _attackDrones = _poolManager.CreateList(prefab, poolSize);
    }

    void Start()
    {
        if(Time.time > _waitToSpawn)
        {
            StartCoroutine("SpawnAttackDrone");
        }
    }


    private IEnumerator SpawnAttackDrone()
    {
        _activeAttackDrone = _poolManager.GetObjectFromPool(_attackDrones);
               
        if(_activeAttackDrone)
        {
            SetObject(spawnPoints, _activeAttackDrone);
        }
        else
        {
            Debug.Log("There are no attackDrones available right now.");
        }

        yield return new WaitForSeconds(_timeBetweenSpawns);
    }
}