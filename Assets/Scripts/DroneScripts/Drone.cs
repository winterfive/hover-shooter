using System;
using UnityEngine;


public class Drone : MonoBehaviour
{
    public int minSpeed, maxSpeed;
    public float xMin, xMax, yMin, yMax, zMin, zMax;
    public Transform[] Spawnpoints;
    public Transform[] EndPoints;
    public float glowSpeed;

    private bool _hasBeenHit;
    private Transform _camTransform = Camera.main.transform;
    private Color _defaultGlow;



    /*
     * Spawns gameObject at random spawnpoint
     * void -> void
     */
    void SpawnDrone() { }


    /*
     * Drone moves to position
     * void -> void
     */
    void Move() { }


    /*
     * Pingpongs drone glow steadily from one color to another
     * void -> void
     */
    private void LerpColor() { }


    /*
     * Creates Vector3 w/ random values for x & z w/in range
     * void -> Vector3
     */
    public void CreateRandomPosition() { }


    /*
     * Returns a random endPoint
     * void -> Vector3
     */
    public void SelectLastPosition() { }
}