using UnityEngine;
using UnityEngine.AI;

public class AttackDroneActions : DroneActions
{
    private Vector3 _camPosition;
    private GameObject _this;
    private NavMeshAgent _agent;
    private AttackDroneValues _ADVRef;
    private bool _isShot;

    public bool IsShot { get; set; }


    void Awake()
    {
        GameObject attackDroneValuesObject = GameObject.FindWithTag("ScriptManager");
        if (attackDroneValuesObject != null)
        {
            _ADVRef = attackDroneValuesObject.GetComponent<AttackDroneValues>();
        }
        else
        {
            Debug.Log("Cannot find AttackDroneValues script");
        }

        _camPosition = Camera.main.transform.position;
        _this = this.gameObject;
        _agent = _this.GetComponent<NavMeshAgent>();
        _isShot = false;
    }


    private void OnEnable()
    {
        if (_agent.isOnNavMesh && _agent.isActiveAndEnabled)
        {
            TravelToRandomPoint();
            _agent.speed = Random.Range(_ADVRef.minSpeed, _ADVRef.maxSpeed);
            _agent.baseOffset = Random.Range(_ADVRef.altitudeMin, _ADVRef.altitudeMax);
        }
        else
        {
            Debug.Log("Attack drone returned to pool (not on navMesh)");
            // Return to pool
            _this.SetActive(false);
        }        
    }


    void Update()
    {
        if(!IsShot)
        {
            // Check if drone is close to destination
            if (Time.frameCount % 30 == 0)
            {
                if (_agent.remainingDistance < _agent.stoppingDistance)
                {
                    TravelToRandomPoint();
                }
            }
        } 
    }


    


    private void TravelToRandomPoint()
    {
        Vector3 point = CreateRandomVector(_ADVRef.xMin, _ADVRef.xMax, 0f, 0f, _ADVRef.xMin, _ADVRef.zMax);
        point.y = _agent.baseOffset;
        _agent.destination = point;
    }
}
