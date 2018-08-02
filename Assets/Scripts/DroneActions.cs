using UnityEngine;
using UnityEngine.AI;


public class DroneActions : MonoBehaviour {

    public float altitudeMin, altitudeMax;
    public float xMin, xMax, zMin, zMax, glowSpeed;
    public Color firstGlow, secondGlow;
    public float minAgentSpeed, maxAgentSpeed;
    
    private Transform _glowTransform;
    private Renderer _glowRend;
    private Transform _turret;
    private NavMeshAgent _agent;
    private Transform _camTransform;
    private float _timeAtSpawn;


    void Start ()
    {
        _agent = GetComponent<NavMeshAgent>();
        _camTransform = Camera.main.gameObject.transform;
        _agent.baseOffset = Random.Range(altitudeMin, altitudeMax);
        _agent.speed = Random.Range(minAgentSpeed, maxAgentSpeed);

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
                GotoPlayer();
            }
        }
    }


    /*
     * Changes drone destination to camera
     * void -> void
     */
    private void GotoPlayer()
    {
        _agent.destination = _camTransform.position;
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


    /*
     * Creates Vector3 w/ random values w/in range
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
        if(_glowRend)
        {
            float pingpong = Mathf.PingPong(Time.time * glowSpeed, 1.0f);
            _glowRend.material.color = Color.Lerp(firstGlow, secondGlow, pingpong);
        }        
    }


    /*
     * Finds child object with tag
     * void -> transform
     */
    private Transform FindChildWithTag(string s)
    {
        Transform firstChild = this.transform.Find("Body");
        Transform[] components = firstChild.GetComponentsInChildren<Transform>();
            
        foreach(Transform t in components)
        {
            if(t.gameObject.CompareTag(s))
            {
                return t;
            }
        }

        return null;
    }

    public void ShootPlayer()
    {
        // TODO
    }
}
