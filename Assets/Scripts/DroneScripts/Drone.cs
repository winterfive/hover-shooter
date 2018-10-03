using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour {

    /*
     * Finds child transform with tag
     * void -> transform
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

    void LerpColor() { }
}
