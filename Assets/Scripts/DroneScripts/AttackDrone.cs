using UnityEngine;
using UnityEngine.AI;

public class AttackDrone : MonoBehaviour
{
    private Vector3 _camPosition;
    private NavMeshAgent _agent;
    private AttackDroneManager _attackDroneManagerReference;
    private bool _movingToMidpoint;
    private Transform _turretTransform;
    private Renderer _glowRenderer;
    private Color _defaultGlowColor;


    void Awake()
    {
        _camPosition = Camera.main.transform.position;
        _agent = this.gameObject.GetComponent<NavMeshAgent>();
        _movingToMidpoint = false;
        _turretTransform = FindChildWithTag("Turret");
        _glowRenderer = FindChildWithTag("Glow").GetComponent<Renderer>();
        _defaultGlowColor = _glowRenderer.material.color;
    }


    void Start()
    {
        GameObject attackDroneManagerObject = GameObject.FindWithTag("ScriptManager");
        if (attackDroneManagerObject != null)
        {
            _attackDroneManagerReference = attackDroneManagerObject.GetComponent<AttackDroneManager>();

            if (_agent.isOnNavMesh && _agent.isActiveAndEnabled)
            {
                SetRandomDestination(); //TODO Move this method to parent
                _agent.speed = Random.Range(_attackDroneManagerReference.minSpeed, _attackDroneManagerReference.maxSpeed);  //TODO make a method for this in parent
                _agent.baseOffset = Random.Range(_attackDroneManagerReference.altitudeMin, _attackDroneManagerReference.altitudeMax);   //TODO "
                _movingToMidpoint = true;
            }
            else
            {
                // TODO using event, send this to the poolmanager for setactive(false)
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
        LerpColor();

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
                    this.gameObject.SetActive(false);
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


    private void SetRandomDestination()
    {
        // TODO Move the guts of this to parent, ahve this method call the method in parent
        // TODO Maybe move to DroneManager
        Vector3 point = _attackDroneManagerReference.CreateRandomDestination();
        point.y = _agent.baseOffset;
        _agent.destination = point;
    }


    private void GoToEndPoint()
    {
        // TODO Move the guts of this to parent, have this method call the method in parent
        // TODO Maybe move to DroneManager
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
        Transform[] components = this.GetComponentsInChildren<Transform>();

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
            float pingpong = Mathf.PingPong(Time.time * _attackDroneManagerReference.glowSpeed, 1.0f);
            _glowRenderer.material.color = Color.Lerp(_defaultGlowColor, _attackDroneManagerReference.secondGlow, pingpong);
        }
    }
}
