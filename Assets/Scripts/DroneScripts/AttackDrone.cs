using UnityEngine;
using UnityEngine.AI;

public class AttackDrone : MonoBehaviour
{
    private Transform _thisTransform;
    private Vector3 _camPosition;
    private NavMeshAgent _agent;
    private AttackDroneManager _attackDroneManagerReference;
    private bool _movingToMidpoint;


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
                SetRandomDestination();
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
     * This implementation only rotates on the y axis
     * Transform -> void
     */
    public void LookAt(Vector3 v)
    {
        if (_thisTransform.gameObject.activeInHierarchy)
        {
            Vector3 newVector = new Vector3(_thisTransform.transform.position.x - v.x,
                                            0f,
                                            _thisTransform.transform.position.z - v.z);

            _thisTransform.transform.rotation = Quaternion.LookRotation(newVector);
        }
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
