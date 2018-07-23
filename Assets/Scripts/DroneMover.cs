using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneMover : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = Camera.main.gameObject.transform.position;
    }
}
