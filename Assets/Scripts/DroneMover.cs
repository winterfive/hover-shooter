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
        glowColor = Color.cyan;
        Debug.Log("object is: " + glowObject.tag);  // It's finding the glow object but color isn't changing

        GotoRandomPoint();

        //StartCoroutine("LerpColor");
        // Lerp is running but not affecting drone clones
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


    //IEnumerator LerpColor()
    //{
    //    glowColor = Color.Lerp(Color.red, Color.blue, 1);
    //    //Color.Lerp(glowColor, Color.blue, Mathf.PingPong(Time.deltaTime, 1));
    //    yield return new WaitForSeconds(3);
    //    //Color.Lerp(Color.blue, glowColor, Time.deltaTime);
    //    //yield return new WaitForSeconds(4);

    //    for (float f = 1f; f >= 0; f -= 0.1f)
    //    {
    //        Color c = glowColor;
    //        c.r = f;
    //        Debug.Log("color value is: " + c.r);
    //        glowColor = c;
    //        yield return new WaitForSeconds(2);
    //    }
    //}

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
