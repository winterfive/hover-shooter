using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Methods for various drone actions except spawning and objectPooling
 */

public class DroneActions : MonoBehaviour {

    /*
     * Sets object at a random starting point
     * spawnPoint[], GameObject -> void
     */
    public void SetAtStart(Transform[] points, GameObject go)
    {
        Transform startPoint = GetRandomValueFromArray(points);
        go.transform.position = startPoint.position;
        go.transform.rotation = startPoint.rotation;
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


    /*
     * Pingpongs color steadily from one color to another
     * Color, Color, float, Renderer -> void
     */
    public void LerpColor(Color color1, Color color2, float glowSpeed, Renderer glow)
    {
        float pingpong = Mathf.PingPong(Time.time * glowSpeed, 1.0f);
        glow.material.color = Color.Lerp(color1, color2, pingpong);
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


    public T GetRandomValueFromArray<T>(T[] arr)
    {
        int index = Random.Range(0, arr.Length);
        T value = arr[index];
        return value;
    }


    public float ReturnRandomValue(float x, float y)
    {
        float newFloat = Random.Range(x, y);
        return newFloat;
    }
}
