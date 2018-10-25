using UnityEngine;
using UnityEngine.AI;

public class AttackDroneMoveGlow : DroneActions
{
    private GameObject _this;
    private NavMeshAgent _agent;
    private AttackDroneValues _ADVRef;
    private bool _isShot;
    private Color _defaultGlowColor;
    private Renderer _glowRend;

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

        _this = this.gameObject;
        _agent = _this.GetComponent<NavMeshAgent>();
        _isShot = false;
        _glowRend = FindChildWithTag("Glow", _this).GetComponent<Renderer>();
        _defaultGlowColor = _glowRend.material.color;
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
            LerpColor(_defaultGlowColor, _ADVRef.secondGlowColor, _ADVRef.glowSpeed, _glowRend);

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
