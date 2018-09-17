using System;
using UnityEngine;


public class Drone : MonoBehaviour
{
    private bool _hasBeenHit;
    private Transform _camTransform = Camera.main.transform;


    /*
     * Spawns gameObject at random spawnpoint
     * void -> void
     */
    void SpawnDrone()
    {
        
    }


    /*
     * Drone moves to position
     * void -> void
     */
     void Move()
    {
        
    }


    /*
     * Pingpongs drone glow steadily from one color to another
     * void -> void
     */
    private void LerpColor()
    {
        
    }


    /*
     * Creates Vector3 w/ random values for x & z w/in range
     * void -> Vector3
     */
    public Vector3 CreateRandomPosition()
    {
        Vector3 randomPosition;

        randomPosition.x = Random.Range(xMin, xMax);
        randomPosition.y = Random.Range(yMin, yMax);
        randomPosition.z = Random.Range(zMin, zMax);

        return randomPosition;
    }


    /*
     * Returns a random endPoint
     * void -> Vector3
     */
    public Vector3 SelectLastPosition()
    {
        int index = Random.Range(0, endPoints.Length);
        Vector3 endPosition = endPoints[index].position;
        return endPosition;
    }
}
