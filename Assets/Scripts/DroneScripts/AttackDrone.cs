using UnityEngine;
using UnityEngine.AI;

public class AttackDrone : Drone
{
    private Vector3 _camPosition;
    private GameObject _this;
    private NavMeshAgent _agent;
    private AttackDroneManager _attackDroneManagerReference;
    private bool _movingToMidpoint;
    private Transform _turretTransform;
    private Renderer _glowRenderer;
    private Color _defaultGlowColor;

    // TODO Every drone will need a stopEverything method based on a bool
    // so that it will stop moving/glowing once it's been shot


    void Awake()
    {
        GameObject attackDroneManagerObject = GameObject.FindWithTag("ScriptManager");
        if (attackDroneManagerObject != null)
        {
            _attackDroneManagerReference = attackDroneManagerObject.GetComponent<AttackDroneManager>();            
        }
        else
        {
            Debug.Log("Cannot find AttackDroneManager script");
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
            _movingToMidpoint = true;
            _agent.speed = _attackDroneManagerReference.GetRandomSpeed();
            _agent.baseOffset = _attackDroneManagerReference.GetRandomOffset();
        }
        else
        {
            Debug.Log("Attack drone returned to pool (not on navMesh)");
            // Return to pool
            _attackDroneManagerReference.SetToInactive(_this);
        }
    }


    void Update()
    {
        LookAt(_camPosition);

        if (_glowRenderer)
        {
            LerpColor(_defaultGlowColor,
                  _attackDroneManagerReference.secondGlowColor,
                  _attackDroneManagerReference.glowSpeed,
                  _glowRenderer);
        }        

        // Check if drone is close to mid point or end point
        if (Time.frameCount % 30 == 0)
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
                    // Return to pool
                    _attackDroneManagerReference.SetToInactive(_this);
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
        Vector3 newVector = new Vector3(_turretTransform.position.x - target.x,
                                        0f,
                                        _turretTransform.position.z - target.z);

        _turretTransform.rotation = Quaternion.LookRotation(newVector);
    }


    private void TravelToMidPoint()
    {
        Vector3 point = _attackDroneManagerReference.SetMidPoint();
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
