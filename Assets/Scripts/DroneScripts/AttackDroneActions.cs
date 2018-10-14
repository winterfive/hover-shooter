using UnityEngine;
using UnityEngine.AI;

public class AttackDroneActions : DroneActions
{
    private Vector3 _camPosition;
    private GameObject _this;
    private NavMeshAgent _agent;
    private AttackDroneValues _ADVRef;
    private bool _movingToMidpoint;
    private Transform _turretTransform;
    private Renderer _glowRenderer;
    private Color _defaultGlowColor;


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
        _movingToMidpoint = false;
        _turretTransform = FindChildWithTag("Turret", _this);
        _glowRenderer = FindChildWithTag("Glow", _this).GetComponent<Renderer>();
        _defaultGlowColor = _glowRenderer.material.color;
    }


    private void Start()
    {
        if (_agent.isOnNavMesh && _agent.isActiveAndEnabled)
        {
            TravelToMidPoint();
            _agent.speed = ReturnRandomValue(_ADVRef.minSpeed, _ADVRef.maxSpeed);
            _agent.baseOffset = ReturnRandomValue(_ADVRef.altitudeMin, _ADVRef.altitudeMax);
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
        LookAt(_camPosition);

        if (_glowRenderer)
        {
            LerpColor(_defaultGlowColor,
                  _ADVRef.secondGlowColor,
                  _ADVRef.glowSpeed,
                  _glowRenderer);
        }        

        //// Check if drone is close to mid point or end point
        //if (Time.frameCount % 30 == 0)
        //{
        //    if (_agent.remainingDistance < _agent.stoppingDistance)
        //    {
        //        if (_movingToMidpoint)
        //        {
        //            GoToEndPoint();
        //            _movingToMidpoint = false;
        //        }
        //        else
        //        {
        //            // Return to pool
        //            _movingToMidpoint = true;
        //            _this.SetActive(false);
        //        }
        //    }
        //}

        // Check if drone is close to destination
        if (Time.frameCount % 30 == 0)
        {
            if (_agent.remainingDistance < _agent.stoppingDistance)
            {
                TravelToMidPoint();
            }
        }
    }

    
     /*
     * Changes position and rotation of turret to look at target using only y axis
     * Vector3 -> void
     */
    public void LookAt(Vector3 target)
    {
        Vector3 newVector = new Vector3(_turretTransform.position.x - target.x,
                                        0f,
                                        _turretTransform.position.z - target.z);

        _turretTransform.rotation = Quaternion.LookRotation(newVector);
    }


    private void TravelToMidPoint()
    {
        Vector3 point = CreateRandomVector(_ADVRef.xMin, _ADVRef.xMax, 0f, 0f, _ADVRef.xMin, _ADVRef.zMax);
        point.y = _agent.baseOffset;
        _agent.destination = point;
    }


    private void GoToEndPoint()
    {
        Vector3 endPoint = GetRandomValueFromArray(_ADVRef.endPoints).position;
        endPoint.y = _agent.baseOffset;
        _agent.destination = endPoint;
    }
}
