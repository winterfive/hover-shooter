using UnityEngine;

/*
 * Handles methods that all drones need
 * - "Spawning" at one of the startPoint transforms
 * - Disabled when shot
 * 
 */

public class DroneActions : SetAsSingleton<DroneActions> {

    /*
     * Sets object at a random starting point
     * spawnPoint[], GameObject -> void
     */
    public void SetAtRandomPoint(Transform[] points, GameObject go)
    {
        Transform startPoint = GetRandomValueFromArray(points);
        go.transform.position = startPoint.position;
        go.transform.rotation = startPoint.rotation;
    }
    

    /*
     * Returns a random vecror3 from a range of values for x, y, z
     * float, float, float, float, float, float -> Vector3
     */
    public Vector3 CreateRandomVector(float xMin, float xMax, float yMin, float yMax, float zMin, float zMax)
    {
        Vector3 randomVector;

        randomVector.x = Random.Range(xMin, xMax);
        randomVector.y = Random.Range(yMin, yMax);
        randomVector.z = Random.Range(zMin, zMax);

        return randomVector;
    }


    /* 
     * Slowly change glow object on drone from one color to another
     * Color, Color, float, Renderer -> void
     */
    public void LerpColor(Color color1, Color color2, float glowSpeed, Renderer glow)
    {
        float pingpong = Mathf.PingPong(Time.time * glowSpeed, 1.0f);
        glow.material.color = Color.Lerp(color1, color2, pingpong);
    }


    public T GetRandomValueFromArray<T>(T[] arr)
    {
        int index = Random.Range(0, arr.Length);
        T value = arr[index];
        return value;
    }


    /*
     * Finds child transform with tag
     * String, GameObject -> Transform
     */
    public Transform FindChildWithTag(string a, GameObject go)
    {
        Transform[] components = go.GetComponentsInChildren<Transform>();

        foreach (Transform t in components)
        {
            if (t.gameObject.CompareTag(a))
            {
                return t;
            }
        }
        return null;
    }
}
