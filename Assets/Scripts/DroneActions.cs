using UnityEngine;
using UnityEngine.AI;

  
public class DroneActions : MonoBehaviour
{
    /*
     * This class is placed on the drone prefab parent object and 
     * handles all actions that are performed differently per each instance.
     * Drone movement (random points to go to)
     * Turret rotation
     * Change of glow color
     * Timing of ShootMissle
     */

    public float altitudeMin, altitudeMax;
    public float glowSpeed;
    public Color secondGlow;
    public float minAgentSpeed, maxAgentSpeed;
    public delegate void MissleFired(Transform t);
    public static MissleFired OnMissleFired;
    public int minTimeBetweenShots, maxTimeBetweenShots;
    public float droneRange;

    private Transform _glowTransform;
    private Renderer _glowRend;
    private Transform _turretTransform;
    private NavMeshAgent _agent;
    private Transform _camTransform;
    private Vector3 _endPoint;
    private Transform _gunTipTransform;
    private RaycastHit _hit;
    private DroneManager _droneManagerReference;
    private ProjectileManager _projectileManagerReference;
    private float _timeOfPreviousShot;
    private float _timeBetweenShots;
    private bool _isShooting;
    private Color _defaultColor;

    public bool IsShooting { get; set; }


    private void Awake()
    {
         _agent = GetComponent<NavMeshAgent>();
        _camTransform = Camera.main.gameObject.transform;
        _agent.baseOffset = Random.Range(altitudeMin, altitudeMax);
        _agent.speed = Random.Range(minAgentSpeed, maxAgentSpeed);

        _turretTransform = FindChildWithTag("Turret");
        _glowTransform = FindChildWithTag("Glow");
        _glowRend = _glowTransform.GetComponent<Renderer>();
        _defaultColor = _glowRend.material.color;
        _gunTipTransform = FindChildWithTag("GunTip");
        _isShooting = true;
    }


    void Start()
    {
        NavMeshHit hit;

        GameObject droneManagerObject = GameObject.FindWithTag("ScriptManager");
        if (droneManagerObject != null)
        {
            _droneManagerReference = droneManagerObject.GetComponent<DroneManager>();
        }

        if (_droneManagerReference == null)
        {
            Debug.Log("Cannot find DroneManager script");
        }

        if (_agent.isOnNavMesh && _agent.isActiveAndEnabled)
        {
            GoToRandomPoint();
        }
        else
        {
            _agent.FindClosestEdge(out hit);
            _agent.Warp(_hit.point);
            Debug.Log("Drone position reset"); // This never outputs
        }
        
        InvokeRepeating("LerpColor", 0f, 0.1f);

        _timeBetweenShots = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        _timeOfPreviousShot = 0f;
    }


    private void Update()
    {        
        LookAtPlayer();

        // Drone shot timing is fixed but each drone has a different time between shots
        if (_isShooting)
        {
            ShootMissles();
        }        
        
        // Check if drone is close to random/mid point
        if (Time.frameCount % 10 == 0)
        {
            if (_agent.remainingDistance < _agent.stoppingDistance || _agent.speed < 0.1)
            {
                GoToEndPoint();
            }            
        }

        // Check if drone is at endPoint
        if (Vector3.Distance(this.transform.position, _endPoint) <= 1.0f || _agent.speed < 0.1)
        {
            this.gameObject.SetActive(false);
        }
    }


    /*
     * Turns drone turret towards player
     * void -> void
     */
    private void LookAtPlayer()
    {
        if (_turretTransform)
        {
            Vector3 newVector = new Vector3(_turretTransform.transform.position.x - _camTransform.position.x,
                                            0f,
                                            _turretTransform.transform.position.z - _camTransform.position.z);

            _turretTransform.transform.rotation = Quaternion.LookRotation(newVector);
        }
    }


    private void ShootMissles()
    {
        if (Time.time > _timeOfPreviousShot + _timeBetweenShots)
        {
            if (Physics.Raycast(_gunTipTransform.position, -_gunTipTransform.forward, out _hit, droneRange))
            {
                if (_hit.transform.tag == "Player")
                {
                    if (OnMissleFired != null)
                    {
                        OnMissleFired(_gunTipTransform);
                        _timeOfPreviousShot = Time.time;
                    }
                }
            }
        }
    }


    /*
     * Pingpongs drone glow steadily from one color to another
     * void -> void
     */
    private void LerpColor()
    {
        if (_glowRend)
        {
            float pingpong = Mathf.PingPong(Time.time * glowSpeed, 1.0f);
            _glowRend.material.color = Color.Lerp(_defaultColor, secondGlow, pingpong);
        }
    }


    /*
     * Assigns and directs drone to random point
     * void -> void
     */
    private void GoToRandomPoint()
    {
        Vector3 midPoint = _droneManagerReference.CreateRandomPosition();
        midPoint.y = _agent.baseOffset;
        _agent.destination = midPoint;        
    }


    /*
     * Assigns and directs drone to end point
     * void -> void
     */
    private void GoToEndPoint()
    {
        _endPoint = _droneManagerReference.SelectLastPosition();
        _endPoint.y = _agent.baseOffset;
        _agent.destination = _endPoint;
    }


    /*
     * Finds grandchild transform with tag
     * void -> transform
     */
    private Transform FindChildWithTag(string a)
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
}