using UnityEngine;
using UnityEngine.AI;


public class DroneActions : MonoBehaviour {

    public float altitudeMin, altitudeMax;
    public float minAgentSpeed, maxAgentSpeed;
    public delegate void ShotFiredAtPlayer();
    public static event ShotFiredAtPlayer OnShotFiredAtPlayer;
    
    private NavMeshAgent _agent;
    private DroneMovement _droneManagerReference;
    private Vector3 _endPoint;
    private Transform _gunTipTransform;
    private RaycastHit _hit;
    

    void Start ()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.baseOffset = Random.Range(altitudeMin, altitudeMax);
        _agent.speed = Random.Range(minAgentSpeed, maxAgentSpeed);
        
        _gunTipTransform = FindChildWithTag("GunTip");


        GameObject droneManagerObject = GameObject.FindWithTag("ScriptManager");
        if (droneManagerObject != null)
        {
            _droneManagerReference = droneManagerObject.GetComponent<DroneMovement>();
        }

        if (_droneManagerReference == null)
        {
            Debug.Log("Cannot find DroneManager script");
        }

        GotoRandomPoint();
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
