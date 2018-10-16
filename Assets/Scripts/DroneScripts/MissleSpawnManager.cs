using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleSpawnManager : PoolingManager {

    private MissleValues _MV;
    private List<GameObject> _missles;
    private Sprite _activeMissle;
    private MissleActions _missleActions;


    private void Awake()
    {
        _MV = MissleValues.Instance;
        _missles = CreateList(_MV.prefab, _MV.poolSize);
    }



}
