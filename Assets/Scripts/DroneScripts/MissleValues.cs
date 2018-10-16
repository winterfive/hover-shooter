using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleValues : SetAsSingleton<MissleValues> {

    public int poolSize;
    public int mSpeed;
    public float minMFireRate, maxMFireRate;
    public GameObject prefab;
    public int droneRange;
}
