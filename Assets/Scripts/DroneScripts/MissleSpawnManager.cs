using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleSpawnManager : PoolingManager {

    private DroneMissleValues _DMV;
    private List<GameObject> _missles;
    private Sprite _activeMissle;
    private MissleActions _missleActions;


    private void Awake()
    {
        _DMV = DroneMissleValues.Instance;
        _missles = CreateList(_DMV.prefab, _DMV.poolSize);
    }



}
