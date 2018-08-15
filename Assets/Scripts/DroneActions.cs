using UnityEngine;
using UnityEngine.AI;


public class DroneActions : MonoBehaviour {

    public float altitudeMin, altitudeMax;
    public float glowSpeed;
    public Color firstGlow, secondGlow;
    public float minAgentSpeed, maxAgentSpeed;
    public delegate void ShotFiredAtPlayer();
    public static event ShotFiredAtPlayer OnShotFiredAtPlayer;
    
    private Transform _glowTransform;
    private Renderer _glowRend;
    private Transform _turret;
    private NavMeshAgent _agent;
    private DroneManager _droneManagerReference;
    private Vector3 _endPoint;
    private Transform _gunTipTransform;
    private RaycastHit _hit;
    

    void Start ()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.baseOffset = Random.Range(altitudeMin, altitudeMax);
        _agent.speed = Random.Range(minAgentSpeed, maxAgentSpeed);
        
        _glowTransform = FindChildWithTag("Glow");
        _glowRend = _glowTransform.GetComponent<Renderer>();
        _gunTipTransform = FindChildWithTag("GunTip");


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
        if (Time.frameCount % 10 == 0)
        {
            if (Physics.Raycast(_gunTipTransform.position, _gunTipTransform.up, out _hit, 1000))
            {
                Debug.DrawRay(_gunTipTransform.position, _gunTipTransform.up);
                if (_hit.transform.tag == "Player")
                {
                    if (OnShotFiredAtPlayer != null)
                    {
                        OnShotFiredAtPlayer();
                    }
                }
            }

            if (_agent.remainingDistance < _agent.stoppingDistance || _agent.speed < 0.1)
            {
                GoToEndPoint();
            }            
        }

        if (Vector3.Distance(this.transform.position, _endPoint) <= 1.0f || _agent.speed < 0.1)
        {
            this.gameObject.SetActive(false);
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
        _endPoint = _droneManagerReference.SelectLastPosition();
        _endPoint.y = _agent.baseOffset;
        _agent.destination = _endPoint;
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
