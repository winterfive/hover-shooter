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
     * float, float, float, float, float, float -> Vector3
     */
    public Vector3 CreateRandomPosition(float xMin, float xMax, float yMin, float yMax, float zMin, float zMax)
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
    public void SelectLastPosition() { }
}