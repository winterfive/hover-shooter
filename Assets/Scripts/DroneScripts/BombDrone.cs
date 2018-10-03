using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BombDrone : Drone
{
    private Vector3 _camPosition;
    private GameObject _this;
    private NavMeshAgent _agent;
    private BombDroneManager _bombDroneManagerReference;
    private Renderer _glowRenderer;
    private Color _defaultGlowColor;
    private Color _otherGlowColor;
    private float _glowSpeed;
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
        //_glowSpeed = _bombDroneManagerReference.glowSpeed;
        //_otherGlowColor = _bombDroneManagerReference.secondGlowColor;
        _detonationDistance = _bombDroneManagerReference.detonationDistance;
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
        //LerpColor();

        if (Time.frameCount % 30 == 0)
        {
            // Check if drone is close to mid point or end point
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


    /*
     * Pingpongs color steadily from one color to another
     * void -> void
     */
    //private void LerpColor()
    //{
    //    if (_glowRenderer)
    //    {
    //        float pingpong = Mathf.PingPong(Time.time * _glowSpeed, 1.0f);
    //        _glowRenderer.material.color = Color.Lerp(_defaultGlowColor, _otherGlowColor, pingpong);
    //    }
    //}
}
