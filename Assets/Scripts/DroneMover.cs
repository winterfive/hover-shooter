using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneMover : MonoBehaviour {

    private Vector3 camPosition;
    

    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        camPosition = Camera.main.gameObject.transform.position;
        agent.destination = camPosition;
    }
}
