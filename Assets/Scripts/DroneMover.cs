using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class DroneMover : MonoBehaviour {

    public float altitudeMin, altitudeMax;
    public float xMin, xMax, zMin, zMax;

    Color lerpedColor;
    Transform glowObject;
    Color glowColor;
    NavMeshAgent agent;
    Transform camTransform;


    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        camTransform = Camera.main.gameObject.transform;
        agent.baseOffset = Random.Range(altitudeMin, altitudeMax);

        glowObject = FindChildWithGlow();
        glowColor = glowObject.GetComponent<Renderer>().material.color;

        GotoRandomPoint();

        StartCoroutine("LerpColor");       
    }


    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
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
        agent.destination = camTransform.position;
    }


    /*
     * Assigns and directs drone to random point
     * void -> void
     */
    void GotoRandomPoint()
    {
        Vector3 newVector = CreateRandomPosition();
        agent.destination = newVector;        
    }


    /*
     * Creates Vector3 w/ random values w/in range
     * void -> Vector3
     */
    private Vector3 CreateRandomPosition()
    {
        Vector3 randomPosition;

        randomPosition.x = Random.Range(xMin, xMax);
        randomPosition.y = agent.baseOffset;
        randomPosition.z = Random.Range(zMin, zMax);

        return randomPosition;
        
    }


    void LerpColor()
    {
        //glowColor = Color.Lerp(Color.red, Color.blue, 1);
        ////Color.Lerp(glowColor, Color.blue, Mathf.PingPong(Time.deltaTime, 1));
        //yield return new WaitForSeconds(5);
        ////Color.Lerp(Color.blue, glowColor, Time.deltaTime);
        ////yield return new WaitForSeconds(4);

    }



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
