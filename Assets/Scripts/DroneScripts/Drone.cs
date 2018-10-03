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

    /*
     * Pingpongs color steadily from one color to another
     * void -> void
     */
    public void LerpColor(Color color1, Color color2, float glowSpeed, Renderer glow)
    {
        float pingpong = Mathf.PingPong(Time.time * glowSpeed, 1.0f);
        glow.material.color = Color.Lerp(color1, color2, pingpong);
    }
}
