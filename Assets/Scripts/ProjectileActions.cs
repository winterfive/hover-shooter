using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileActions : MonoBehaviour
{

    public int missleSpeed;
    
    private GameObject _missle;
    private Transform _camTransform;


    // Use this for initialization
    void Awake()
    {
        _camTransform = Camera.main.transform;
        _missle = this.gameObject;
    }

    void Update()
    {
        // if (missle has hit player)
        //{
            // set active to false
            // call PLayerHit()
        //}

        if (Vector3.Distance(_missle.transform.position, _camTransform.position) > 0.5)
        {
            float step = missleSpeed * Time.deltaTime;
            _missle.transform.position = Vector3.MoveTowards(_missle.transform.position, _camTransform.position, step);
        }
    }
}
