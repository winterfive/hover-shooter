using UnityEngine;
using UnityEngine.AI;

public class BombDroneActions : DroneActions
{
    private Vector3 _camPosition;
    private GameObject _this;
    private NavMeshAgent _agent;
    private BombDroneValues _BDVRef;
    private bool _movingToMidpoint;
    //private Renderer _glowRenderer;
    //private Color _defaultGlowColor;


    void Awake()
    {
        GameObject bombDroneValuesObject = GameObject.FindWithTag("ScriptManager");
        if (bombDroneValuesObject != null)
        {
            _BDVRef = bombDroneValuesObject.GetComponent<BombDroneValues>();
        }
        else
        {
            Debug.Log("Cannot find BombDroneValues script");
        }

        _camPosition = Camera.main.transform.position;
        _this = this.gameObject;
        _agent = _this.GetComponent<NavMeshAgent>();
        _movingToMidpoint = false;
        //_glowRenderer = FindChildWithTag("Glow", _this).GetComponent<Renderer>();
        //_defaultGlowColor = _glowRenderer.material.color;
    }


    private void Start()
    {
        if (_agent.isOnNavMesh && _agent.isActiveAndEnabled)
        {
            TravelToMidPoint();
            _movingToMidpoint = true;
            _agent.speed = ReturnRandomValue(_BDVRef.minSpeed, _BDVRef.maxSpeed);
            _agent.baseOffset = ReturnRandomValue(_BDVRef.altitudeMin, _BDVRef.altitudeMax);
        }
        else
        {
            Debug.Log("Bomb drone returned to pool (not on navMesh)");
            // Return to pool
            _this.SetActive(false);
        }
    }


    void Update()
    {
        //if (_glowRenderer)
        //{
        //    LerpColor(_defaultGlowColor,
        //          _BDVRef.secondGlowColor,
        //          _BDVRef.glowSpeed,
        //          _glowRenderer);
        //}

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
                    _this.SetActive(false);
                }
            }
        }
    }


    private void TravelToMidPoint()
    {
        Vector3 point = CreateRandomVector(_BDVRef.xMin, _BDVRef.xMax, 0f, 0f, _BDVRef.xMin, _BDVRef.zMax);
        point.y = _agent.baseOffset;
        _agent.destination = point;
    }


    private void GoToEndPoint()
    {
        Vector3 endPoint = _camPosition;
        endPoint.y = _agent.baseOffset;
        _agent.destination = endPoint;
    }
}
