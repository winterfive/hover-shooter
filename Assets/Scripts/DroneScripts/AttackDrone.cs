using UnityEngine;
using UnityEngine.AI;

public class AttackDrone : MonoBehaviour
{
    private Vector3 _camPosition;
    private GameObject _this;
    private NavMeshAgent _agent;
    private AttackDroneManager _attackDroneManagerReference;
    private bool _movingToMidpoint;
    private Transform _turretTransform;
    private Renderer _glowRenderer;
    private Color _defaultGlowColor;
    private Color _otherGlowColor;
    private float _glowSpeed;

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
            // Return to pool
            _attackDroneManagerReference.SetToInactive(_this);
        }

        _camPosition = Camera.main.transform.position;
        _this = this.gameObject;
        _agent = _this.GetComponent<NavMeshAgent>();
        _movingToMidpoint = false;
        _turretTransform = FindChildWithTag("Turret");
        _glowRenderer = FindChildWithTag("Glow").GetComponent<Renderer>();
        _defaultGlowColor = _glowRenderer.material.color;
        _glowSpeed = _attackDroneManagerReference.glowSpeed;
        _otherGlowColor = _attackDroneManagerReference.secondGlowColor;
    }


    private void Start()
    {
        if (_agent.isOnNavMesh && _agent.isActiveAndEnabled)
        {
            SetMidPoint();
            _agent.speed = _attackDroneManagerReference.GetRandomSpeed();
            _agent.baseOffset = _attackDroneManagerReference.GetRandomOffset();
            
            _movingToMidpoint = true;
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
        LerpColor();

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


    private void SetMidPoint()
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


    /*
     * Finds child transform with tag
     * void -> transform
     */
    public Transform FindChildWithTag(string a)
    {
        Transform[] components = _this.GetComponentsInChildren<Transform>();

        foreach (Transform t in components)
        {
            if (t.gameObject.CompareTag(a))
            {
                return t;
            }
        }
        return null;
    }


    /*
     * Pingpongs color steadily from one color to another
     * void -> void
     */
    private void LerpColor()
    {
        if (_glowRenderer)
        {
            float pingpong = Mathf.PingPong(Time.time * _glowSpeed, 1.0f);
            _glowRenderer.material.color = Color.Lerp(_defaultGlowColor, _otherGlowColor, pingpong);
        }
    }
}
