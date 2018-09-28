using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDroneManager : PooledObjectManager {

    /*
     * This class handles values & methods needed for spawning and moving attackDrones
     */

    public Transform[] spawnPoints;
    public Transform[] endPoints;
    public GameObject prefab;
    public int poolSize;
    public float xMin, xMax, yMin, yMax, zMin, zMax;
    public float minSpeed, maxSpeed;
    public float altitudeMin, altitudeMax;
    public float timeBetweenSpawns;
    public float waitToSpawn;
    public float glowSpeed;
    public Color secondGlowColor;
    public int damageValue, pointValue;
    public Color destroyColor1, destroyColor2, destroyColor3;
    public float waitBetweenColors;

    private PoolManager _poolManager;
    private List<GameObject> _attackDrones;
    private GameObject _activeAttackDrone;
    private PlayerManager _playerManager;
    private Color[] _destructionColors;


    private void Awake()
    {
        _poolManager = PoolManager.Instance;
        _playerManager = PlayerManager.Instance;
        _attackDrones = _poolManager.CreateList(prefab, poolSize);
    }


    void Start()
    {
        _destructionColors[0] = destroyColor1;
        _destructionColors[1] = destroyColor2;
        _destructionColors[2] = destroyColor3;
        StartCoroutine(SpawnAttackDrone());
    }


    /*
     * Spawns attack drone at one of five starting points
     * void -> void
     */
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

            yield return new WaitForSeconds(timeBetweenSpawns);
        }            
    }


    public Vector3 SetMidPoint()
    {
        Vector3 mid = CreateRandomVector(xMin, xMax, yMin, yMax, zMin, zMax);
        return mid;
    }


    public Vector3 SetEndPoint()
    {
        Vector3 end = GetRandomValueFromArray(endPoints).position;
        return end;
    }


    public float GetRandomSpeed()
    {
        float value = Random.Range(minSpeed, maxSpeed);
        return value;
    }


    public float GetRandomOffset()
    {
        float o = Random.Range(altitudeMin, altitudeMax);
        return o;
    }


    public void SetToInactive(GameObject go)
    {
        ReturnToPool(go);
    }
}