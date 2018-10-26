using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDroneValues : SetAsSingleton<BombDroneValues> {

    /*
     * This class holds values needed by the BombDrone class
     */

    public Transform[] spawnPoints;
    public GameObject prefab;
    public int poolSize;
    public float xMin, xMax, yMin, yMax, zMin, zMax;
    public float minSpeed, maxSpeed;
    public float altitudeMin, altitudeMax;
    public float timeBetweenSpawns;
    public float timeBeforeInitialSpawn;
    public float glowSpeed;
    public Color secondGlowColor;
    public int damageValue, pointValue;
    public int proximityToPlayer;
    public Camera cam;
}