using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleValues : SetAsSingleton<DroneMissleValues> {

    public int poolSize;
    public int minMSPeed, maxMSpeed;
    public int minMFireRate, maxMFireRate;
    public GameObject prefab;

}
