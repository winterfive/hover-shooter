using System.Collections.Generic;
using UnityEngine;

public class DroneManager : GenericManager<DroneManager>
{
    /*
     * This class comment... TODO
     */


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
}

