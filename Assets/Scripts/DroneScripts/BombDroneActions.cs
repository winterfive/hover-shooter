using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BombDroneActions : DroneActions
{
    private Vector3 _camPosition;
    private GameObject _this;
    private NavMeshAgent _agent;
    private BombDroneManager _bombDroneManagerReference;
    private Renderer _glowRenderer;
    private Color _defaultGlowColor;
    private bool _isAlive;
    private int _detonationDistance;
    private bool _movingToMidPoint;

    // TODO Every drone will need a stopEverything method based on a bool
    // so that it will stop moving/glowing once it's been shot


    void Awake()
    {
        GameObject bombDroneManagerObject = GameObject.FindWithTag("ScriptManager");
        if (bombDroneManagerObject != null)
        {
            _bombDroneManagerReference = bombDroneManagerObject.GetComponent<BombDroneManager>();            
        }
        else
        {
            Debug.Log("Cannot find AttackDroneManager script");
        }

        _camPosition = Camera.main.transform.position;
        _this = this.gameObject;
        _agent = _this.GetComponent<NavMeshAgent>();
        //_glowRenderer = FindChildWithTag("Glow", _this).GetComponent<Renderer>();
        //_defaultGlowColor = _glowRenderer.material.color;
        _isAlive = true;
    }


    private void Start()
    {
        if (_agent.isOnNavMesh && _agent.isActiveAndEnabled)
        {
            AssignMidPoint();
            _movingToMidPoint = true;
            _agent.speed = _bombDroneManagerReference.GetRandomSpeed();
            _agent.baseOffset = _bombDroneManagerReference.GetRandomOffset();
        }
        else
        {
            Debug.Log("Attack drone returned to pool (not on navMesh)");
            // Return to pool
            _bombDroneManagerReference.SetToInactive(_this);
        }
    }


    void Update()
    {
        if (_glowRenderer)
        {
            //LerpColor(_defaultGlowColor, 
            //          _bombDroneManagerReference.secondGlowColor,
            //          _bombDroneManagerReference.glowSpeed,
            //          _glowRenderer);
        }

        if (Time.frameCount % 30 == 0)
        {
            if (_agent.remainingDistance < _agent.stoppingDistance)
            {
                if (_movingToMidPoint)
                {
                    GoToEndPoint();
                    _movingToMidPoint = false;
                }
                else
                {
                    // Return to pool
                    _bombDroneManagerReference.SetToInactive(_this);
                }
            }
        }
    }


    private void AssignMidPoint()
    {
        Vector3 point = _bombDroneManagerReference.SetMidPoint();
        point.y = _agent.baseOffset;
        _agent.destination = point;
    }


    private void GoToEndPoint()
    {
        Vector3 endPoint = _bombDroneManagerReference.SetEndPoint();
        endPoint.y = _agent.baseOffset;
        _agent.destination = endPoint;
    }
}
