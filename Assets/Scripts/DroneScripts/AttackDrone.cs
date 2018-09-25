using System;
using UnityEngine;
using UnityEngine.AI;

public class AttackDrone : MonoBehaviour
{
    private Transform _thisTransform;
    private Vector3 _camPosition;
    private NavMeshAgent _agent;
    private AttackDroneManager _attackDroneManagerReference;
    private bool _movingToMidpoint;
    private Transform turretTransform;


    void Awake()
    {
        _camPosition = Camera.main.transform.position;
        _thisTransform = this.gameObject.transform;
        _agent = this.gameObject.GetComponent<NavMeshAgent>();
        _movingToMidpoint = false;
    }


    void Start()
    {
        GameObject attackDroneManagerObject = GameObject.FindWithTag("ScriptManager");
        if (attackDroneManagerObject != null)
        {
            _attackDroneManagerReference = attackDroneManagerObject.GetComponent<AttackDroneManager>();

            if (_agent.isOnNavMesh && _agent.isActiveAndEnabled)
            {
                turretTransform = _attackDroneManagerReference.FindChildWithTag("turret").transform;
                SetRandomDestination();
                _agent.speed = Random.Range(_attackDroneManagerReference.minSpeed, _attackDroneManagerReference.maxSpeed);
                _agent.baseOffset = Random.Range(_attackDroneManagerReference.altitudeMin, _attackDroneManagerReference.altitudeMax);
                _movingToMidpoint = true;
            }
            else
            {
                _agent.gameObject.SetActive(false);
                Debug.Log("Attack drone returned to pool (not on navMesh)");
            }
        }

        if (_attackDroneManagerReference == null)
        {
            Debug.Log("Cannot find AttackDroneManager script");
        }        
    }


    void Update()
    {
        LookAt(_camPosition);

        // Check if drone is close to mid point or end point
        if (Time.frameCount % 10 == 0)
        {
            if (_agent.remainingDistance < _agent.stoppingDistance)
            {
                if (_movingToMidpoint)
                {
                    GoToEndPoint();
                    _movingToMidpoint = false;
                }
                else
                {
                    _thisTransform.gameObject.SetActive(false);
                }
            }
        }
    }

    
     /*
     * Changes position and rotation of turret to look at target using only y axis
     * Vector3 -> void
     */
    public void LookAt(Vector3 target)
    {
        Vector3 newVector = new Vector3(turretTransform.position.x - target.x,
                                        0f,
                                        turretTransform.position.z - target.z);

        turretTransform.rotation = Quaternion.LookRotation(v);
    }


    private void SetRandomDestination()
    {
        Vector3 point = _attackDroneManagerReference.CreateRandomDestination();
        point.y = _agent.baseOffset;
        _agent.destination = point;
    }


    private void GoToEndPoint()
    {
        Vector3 endPoint = _attackDroneManagerReference.SetEndPoint();
        endPoint.y = _agent.baseOffset;
        _agent.destination = endPoint;
    }
}
