using UnityEngine;
using UnityEngine.AI;

public class AttackDrone : Drone, ILookAt<Transform>
{
    public float xMin, xMax, yMin, yMax, zMin, zMax;

    private Transform _camTransform;
    private Transform _thisTransform;
    private NavMeshAgent _agent;

    void Awake()
    {
        _camTransform = Camera.main.transform;
        _thisTransform = this.gameObject.transform;
        _agent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        _agent.destination = CreateRandomPosition(xMin, xMax, yMin, yMax, zMin, zMax);
    }

    void Update()
    {
        LookAt(_camTransform);

        if(_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _agent.destination = ReturnRandomValueFromArray(endPoints).position;
        }
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
}
