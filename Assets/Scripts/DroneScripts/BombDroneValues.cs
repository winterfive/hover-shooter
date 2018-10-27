using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDroneValues : SetAsSingleton<BombDroneValues> {

    /*
     * This class holds values needed by the BombDrone class
     */

    public Transform[] spawnPoints;
    public GameObject prefab;
    public int poolSize = 1;    // Only 1 bombDrone at a time
    public float xMin, xMax, yMin, yMax, zMin, zMax;
    public float minSpeed, maxSpeed;
    public float altitudeMin, altitudeMax;
    [Range(3f, 6f)]
    public float timeBetweenSpawns;
    [Range(4f, 10f)]
    public float timeBeforeInitialSpawn;
    public float glowSpeed;
    public Color secondGlowColor;
    public int damageValue, pointValue;
    public Camera cam;
}