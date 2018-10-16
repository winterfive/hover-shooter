using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleValues : SetAsSingleton<MissleValues> {

    public int poolSize;
    public int missleSpeed;
    public float minMissleFireRate, maxMissleFireRate;
    public GameObject prefab;
    public int droneRange;
}
