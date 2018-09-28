using System.Collections.Generic;
using UnityEngine;

public class PooledObjectManager : GenericManager<PooledObjectManager>
{
    /*
     * Parent class for all pooled objects
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


    /*
     * Returns object to its pool
     * This is the ONLY method for return in the project
     * GameObject -> void
     */
    public void ReturnToPool(GameObject go)
    {
        go.SetActive(false);
    }
}

