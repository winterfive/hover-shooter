using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class DroneActions : MonoBehaviour {

    public float altitudeMin, altitudeMax;
    public float xMin, xMax, zMin, zMax, glowSpeed;
    public Color firstGlow, secondGlow;
    public float minAgentSpeed, maxAgentSpeed;
    
    private Transform _glowTransform;
    private Renderer _glowRend;
    private NavMeshAgent _agent;
    private Transform _camTransform;
    private float _timeAtSpawn;


    void Start ()
    {
        _agent = GetComponent<NavMeshAgent>();
        _camTransform = Camera.main.gameObject.transform;
        _agent.baseOffset = Random.Range(altitudeMin, altitudeMax);
        _agent.speed = Random.Range(minAgentSpeed, maxAgentSpeed);

        _glowTransform = FindChildWithGlow();
        _glowRend = _glowTransform.GetComponent<Renderer>();

        GotoRandomPoint();

        InvokeRepeating("LerpColor", 0f, 0.1f);
    }


    private void Update()
    {
        if (Time.frameCount % 5 == 0)
        {
            _agent.speed = Random.Range(minAgentSpeed, maxAgentSpeed);
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
     * Drone turret always pointed at player
     * void -> void
     */
     void LookAtPlayer()
    {
        // TODO
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


    void LerpColor()
    {
        float pingpong = Mathf.PingPong(Time.time * glowSpeed, 1.0f);

        _glowRend.material.color = Color.Lerp(firstGlow, secondGlow, pingpong);
    }


    /*
     * Finds child object with "Glow" tag
     * void -> transform
     */
    private Transform FindChildWithGlow()
    {
        Transform firstChild = this.transform.Find("Body");
        Transform[] components = firstChild.GetComponentsInChildren<Transform>();
            
        foreach(Transform t in components)
        {
            if(t.gameObject.CompareTag("Glow"))
            {
                return t;
            }
        }

        return null;
    }
}
