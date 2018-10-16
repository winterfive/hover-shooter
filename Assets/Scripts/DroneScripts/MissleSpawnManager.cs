using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleSpawnManager : PoolingManager {

    private MissleValues _MV;
    private List<GameObject> _missles;
    private GameObject _activeMissle;
    private MissleActions _missleActions;


    private void Awake()
    {
        _MV = MissleValues.Instance;
        _missles = CreateList(_MV.prefab, _MV.poolSize);
    }


    private void SpawnMissle(Transform gunTip)
    {
        _activeMissle = GetObjectFromPool(_missles);
        _activeMissle.transform.position = gunTip.position;
        _activeMissle.transform.rotation = gunTip.rotation;
        _activeMissle.SetActive(true);
    }


    private void OnEnable()
    {
        DroneShooting.OnMissleFired += SpawnMissle;
    }


    private void OnDisable()
    {
        DroneShooting.OnMissleFired -= SpawnMissle;
    }
}
