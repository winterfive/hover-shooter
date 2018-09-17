using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackDrone : Drone, ILookAt<Transform>
{
    public Transform[] spawnPoints;
    public Transform[] endPoints;
    public new float xMin, xMax, yMin, yMax, zMin, zMax;

    private Transform _camTransform;
    private Transform _thisTransform;
    private NavMeshAgent _agent;

    void Awake()
    {
        _camTransform = Camera.main.transform;
        _thisTransform = this.gameObject.transform;
        _agent = this.gameObject.GetComponent<NavMeshAgent>();
        _agent.destination = CreateRandomPosition(xMin, xMax, yMin, yMax, zMin, zMax);
    }

    void Start()
    {
        
    }

    void Update()
    {
        LookAt(_camTransform);
    }


    /*
     * This implementation only rotates on the y axis
     * Transform -> void
     */
    public void LookAt(Transform t)
    {
        if (_thisTransform)
        {
            Vector3 newVector = new Vector3(_thisTransform.transform.position.x - t.position.x,
                                            0f,
                                            _thisTransform.transform.position.z - t.position.z);

            _thisTransform.transform.rotation = Quaternion.LookRotation(newVector);
        }
    }


    public new Vector3 SelectLastPosition()
    {
        int index = Random.Range(0, endPoints.Length);
        Vector3 endPosition = endPoints[index].position;
        return endPosition;
    }
}
