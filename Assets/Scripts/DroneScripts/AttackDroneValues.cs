using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDroneValues : SetAsSingleton<AttackDroneValues> {

    /*
     * This class holds values needed by the attackDrone class
     */

    public Transform[] spawnPoints;
    public GameObject prefab;
    [Range(10, 30)]
    public int poolSize;
    public float xMin, xMax, yMin, yMax, zMin, zMax;
    public float minSpeed, maxSpeed;
    public float altitudeMin, altitudeMax;
    [Range(0.5f,2f)]
    public float timeBetweenSpawns;
    [Range(3f, 8f)]
    public float waitToSpawn;
    public float glowSpeed;
    public Color secondGlowColor;
    public int damageValue, pointValue;
}