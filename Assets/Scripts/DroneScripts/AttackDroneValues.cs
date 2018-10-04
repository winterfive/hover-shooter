using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDroneValues : GenericManager<AttackDroneValues> {

    /*
     * This class holds values needed by the attackDrone class
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
}