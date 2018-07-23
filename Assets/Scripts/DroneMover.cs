using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneMover : MonoBehaviour {

	void Start ()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = Camera.main.gameObject.transform.position;
        agent.baseOffset = Random.Range(0, 7);

        // https://docs.unity3d.com/Manual/nav-AgentPatrol.html
        // Have agent follwo preset array of points
        // Array is randomly choosen
        // Create 4 arrays, each witha different path

    }
}
