using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleValues : SetAsSingleton<MissleValues> {

    public int poolSize;
    public int minMSPeed, maxMSpeed;
    public float minMFireRate, maxMFireRate;
    public GameObject prefab;
    public int droneRange;

}
