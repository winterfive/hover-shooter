using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DroneMover : MonoBehaviour {

    public float altitudeMin, altitudeMax;
    public float xMin, xMax, zMin, zMax;

    Color lerpedColor;
    Color glowColor;
    NavMeshAgent agent;
    Transform camTransform;


    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        camTransform = Camera.main.gameObject.transform;
        glowColor = this.gameObject.GetComponentInChildren<Renderer>().material.GetColor("_Color"); // Not getting right component?
        agent.baseOffset = UnityEngine.Random.Range(altitudeMin, altitudeMax);
        GotoRandomPoint();
        // StartCoroutine("LerpColor");
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

    IEnumerator LerpColor()
    {
            lerpedColor = Color.Lerp(Color.red, Color.blue, Time.deltaTime);
            glowColor = lerpedColor;
            yield return new WaitForSeconds(4);
            lerpedColor = Color.Lerp(Color.blue, Color.red, Time.deltaTime);
            glowColor = lerpedColor;
            yield return new WaitForSeconds(4);
    }
}
