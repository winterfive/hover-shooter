using UnityEngine;
using UnityEngine.AI;


public class DroneActions : MonoBehaviour {

    public float altitudeMin, altitudeMax;
    public float glowSpeed;
    public Color firstGlow, secondGlow;
    public float minAgentSpeed, maxAgentSpeed;    

    private Transform _glowTransform;
    private Renderer _glowRend;
    private Transform _turret;
    private NavMeshAgent _agent;
    private Transform _camTransform;
    private LineRenderer line;
    private DroneManager _droneManagerReference;
    private Vector3 endPoint;


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

        GameObject droneManagerObject = GameObject.FindWithTag("ScriptManager");
        
        if (droneManagerObject != null)
        {
            _droneManagerReference = droneManagerObject.GetComponent<DroneManager>();
        }
        
        if (_droneManagerReference == null)
        {
            Debug.Log("Cannot find DroneManager script");
        }

        GotoRandomPoint();

        InvokeRepeating("LerpColor", 0f, 0.1f);
    }


    private void Update()
    {
        LookAtPlayer();

        if (Time.frameCount % 10 == 0)
        {
            ShootAtPlayer();
        }

        if (Time.frameCount % 2 == 0)
        {
            if (_agent.remainingDistance < _agent.stoppingDistance || _agent.speed == 0)
            {
                GoToEndPoint();
            }
        }

        if (Vector3.Distance(this.transform.position, endPoint) <= 1.0f)
        {
            _droneManagerReference.ReturnToPool(this.gameObject);
        }        
    }    


    /*
     * Assigns and directs drone to random point
     * void -> void
     */
    void GotoRandomPoint()
    {
        Vector3 midPoint = _droneManagerReference.CreateRandomPosition();
        midPoint.y = _agent.baseOffset;
        _agent.destination = midPoint;
    }


    /*
     * Assigns and directs drone to end point
     * void -> void
     */
    void GoToEndPoint()
    {
        endPoint = _droneManagerReference.GetEndPosition();
        endPoint.y = _agent.baseOffset;
        _agent.destination = endPoint;
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
    public void ShootAtPlayer()
    {
        RaycastHit _hit;

        Transform gunTip = FindChildWithTag("GunTip");

        line.enabled = true;

        //Debug.DrawRay(gunTip.transform.position, gunTip.up * 10, Color.green, 2);  THIS WORKS
       
        if (Physics.Raycast(gunTip.transform.position, gunTip.transform.up, out _hit, 150))
        {
            if (_hit.transform.gameObject.tag == "MainCamera")
            {
                line.SetPosition(0, gunTip.transform.position);
                line.SetPosition(1, _hit.point);
            }
        }
    }
}
