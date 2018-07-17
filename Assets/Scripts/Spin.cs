using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    public float speed;


    private void FixedUpdate()
    {
        gameObject.transform.Rotate(0f, speed, 0f, Space.Self);
    }
}
