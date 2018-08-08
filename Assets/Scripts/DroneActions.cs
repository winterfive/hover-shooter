using UnityEngine;
using UnityEngine.AI;


public class DroneActions : MonoBehaviour {

    public float altitudeMin, altitudeMax;
    public float xMin, xMax, zMin, zMax, glowSpeed;
    public Color firstGlow, secondGlow;
    public float minAgentSpeed, maxAgentSpeed;
    public float lineStartWidth, lineEndWidth, lineTolerance;
    public DroneManager droneManager;

    private Transform _glowTransform;
    private Renderer _glowRend;
    private Transform _turret;
    private NavMeshAgent _agent;
    private Transform _camTransform;
    private LineRenderer line;


    void Start ()
    {
        _agent = GetComponent<NavMeshAgent>();
        _camTransform = Camera.main.gameObject.transform;
        _agent.baseOffset = Random.Range(altitudeMin, altitudeMax);
        _agent.speed = Random.Range(minAgentSpeed, maxAgentSpeed);

        line = GetComponent<LineRenderer>();

        _turret = FindChildWithTag("Turret");
        _glowTransform = FindChildWithTag("Glow");
        _glowRend = _glowTransform.GetComponent<Renderer>();

        GotoRandomPoint();

        InvokeRepeating("LerpColor", 0f, 0.1f);
    }


    private void Update()
    {
        LookAtPlayer();

        if (Time.frameCount % 10 == 0)
        {
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            {
                GoToEndPoint();
            }

            ShootPlayer();
        }
    }    


    /*
     * Assigns and directs drone to random point
     * void -> void
     */
    void GotoRandomPoint()
    {
        Vector3 midPoint = CreateRandomPosition();
        _agent.destination = midPoint;
    }


    void GoToEndPoint()
    {
        Vector3 endPoint = droneManager.GetEndPosition();
        endPoint.y = _agent.baseOffset;
        _agent.destination = endPoint;
    }


    /*
     * Creates Vector3 w/ random values for x & z w/in range
     * void -> Vector3
     */
    private Vector3 CreateRandomPosition()
    {
        Vector3 randomPosition;

        randomPosition.x = Random.Range(xMin, xMax);
        randomPosition.y = _agent.baseOffset;
        randomPosition.z = Random.Range(zMin, zMax);

        return randomPosition;        
    }


    /*
     * Turns drone turret towards player
     * void -> void
     */
    private void LookAtPlayer()
    {
        if (_turret)
        {
            Vector3 newVector = new Vector3(_turret.transform.position.x - _camTransform.position.x,
                                            0f,
                                            _turret.transform.position.z - _camTransform.position.z);

            _turret.transform.rotation = Quaternion.LookRotation(newVector);
        }        
    }

    
    /*
     * Pingpongs drone glow steadily from one color to another
     * void -> void
     */
    void LerpColor()
    {
        if (_glowRend)
        {
            float pingpong = Mathf.PingPong(Time.time * glowSpeed, 1.0f);
            _glowRend.material.color = Color.Lerp(firstGlow, secondGlow, pingpong);
        }        
    }


    /*
     * Finds grandchild transform with tag
     * void -> transform
     */
    private Transform FindChildWithTag (string a)
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
     * Fires at player position
     * void -> void
     */
    public void ShootPlayer()
    {
        RaycastHit _hit;
        line.startWidth = lineStartWidth;
        line.endWidth = lineEndWidth;
        line.Simplify(lineTolerance);

        Transform gunTip = FindChildWithTag("Glow");

       
        if (Physics.Raycast(gunTip.position, transform.forward, out _hit, 100))
        {
            if (_hit.transform.gameObject.tag == "Player")
            {
                line.enabled = true;
            }
        }
    }
}
