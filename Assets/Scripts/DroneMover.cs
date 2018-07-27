using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class DroneMover : MonoBehaviour {

    public float altitudeMin, altitudeMax;
    public float xMin, xMax, zMin, zMax, speed;

    private DroneMover myDroneReference;
    private Transform _glowTransform;
    private Renderer _glowRenderer;
    private NavMeshAgent _agent;
    private Transform _camTransform;


    void Start ()
    {
        GameObject drone = GameObject.FindWithTag("Drone");

        if(drone != null)
        {
            myDroneReference = drone.GetComponent<DroneMover>();
        }

        if(myDroneReference == null)
        {
            Debug.Log("Cannot find DroneMover script");
        }

        _agent = GetComponent<NavMeshAgent>();
        _camTransform = Camera.main.gameObject.transform;
        _agent.baseOffset = Random.Range(altitudeMin, altitudeMax);        

        _glowTransform = FindChildWithGlow();
        _glowRenderer = _glowTransform.GetComponent<Renderer>();

        GotoRandomPoint();

        InvokeRepeating("LerpColor", 0f, 1f);
    }


    private void Update()
    {
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            GotoPlayer();
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
        Vector3 newVector = CreateRandomPosition();
        _agent.destination = newVector;        
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
        Color glowColor = _glowRenderer.material.color;
        Color altGlowColor = Color.cyan;

        _glowRenderer.material.color = Color.Lerp(glowColor, altGlowColor, Mathf.PingPong(Time.time * speed, 1));
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
