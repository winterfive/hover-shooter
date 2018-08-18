using UnityEngine;
using UnityEngine.AI;

  
public class DroneActions : MonoBehaviour
{
    /*
     * This class handles all actions based on local drone data
     * Drone movement, turret rotation, change of glow color.
     */

    public float altitudeMin, altitudeMax;
    public float glowSpeed;
    public Color secondGlow;
    public float minAgentSpeed, maxAgentSpeed;

    private Transform _glowTransform;
    private Renderer _glowRend;
    private Transform _turretTransform;
    private NavMeshAgent _agent;
    private Transform _camTransform;
    private Vector3 _endPoint;
    private Transform _gunTipTransform;
    private RaycastHit _hit;


    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _camTransform = Camera.main.gameObject.transform;
        _agent.baseOffset = Random.Range(altitudeMin, altitudeMax);
        _agent.speed = Random.Range(minAgentSpeed, maxAgentSpeed);

        _turretTransform = FindChildWithTag("Turret");
        _glowTransform = FindChildWithTag("Glow");
        _glowRend = _glowTransform.GetComponent<Renderer>();
        _gunTipTransform = FindChildWithTag("GunTip");

        GotoRandomPoint();
        InvokeRepeating("LerpColor", 0f, 0.1f);
    }


    private void Update()
    {
        LookAtPlayer();

        if (Time.frameCount % 50 == 0)
        { 
            if (Physics.Raycast(_gunTipTransform.position, -_gunTipTransform.forward, out _hit, 125))
            {
                if (_hit.transform.tag == "Player")
                {
                    // TODO Change to event, call to ProjectileManager
                    _droneManagerReference.ShootMissle(_turretTransform);
                }
            }
        }

        if (Time.frameCount % 10 == 0)
        {
            if (_agent.remainingDistance < _agent.stoppingDistance || _agent.speed < 0.1)
            {
                GoToEndPoint();
            }            
        }

        if (Vector3.Distance(this.transform.position, _endPoint) <= 1.0f || _agent.speed < 0.1)
        {
            // TODO call event to poolManager to setActive(false);
            _droneManagerReference.ReturnToPool(this.gameObject);
        }
    }


    /*
     * Assigns and directs drone to random point
     * void -> void
     */
    private void GotoRandomPoint()
    {
        // TODO Add method to DroneActions?
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
        // TODO Add method to DroneActions?
        _endPoint = _droneManagerReference.SelectLastPosition();
        _endPoint.y = _agent.baseOffset;
        _agent.destination = _endPoint;
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


    /*
     * Pingpongs drone glow steadily from one color to another
     * void -> void
     */
    private void LerpColor()
    {
        Color defaultColor = _glowRend.material.color;

        if (_glowRend)
        {
            float pingpong = Mathf.PingPong(Time.time * glowSpeed, 1.0f);
            _glowRend.material.color = Color.Lerp(defaultColor, secondGlow, pingpong);
        }
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