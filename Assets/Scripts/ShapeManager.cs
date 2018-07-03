using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    public GameObject[] Shapes;
    public Transform[] SpawnPoints;
    public float spawnTime;
    public float colorAdd;
    public RaycastManager raycastManager;
    public Material cubeNormal;
    public Material cubeOver;
    public Material sphereNormal;
    public Material sphereOver;

    private GameObject managers;
    private GameObject _objectFound;

    //  Use this for initialization
    void Start ()
    {
        InvokeRepeating("SpawnShapes", spawnTime, spawnTime);
	}

    private void Update()
    {
        if(raycastManager.HasHitObject && raycastManager.IfShootable)
        {
            _objectFound = raycastManager.GetObjectFound.gameObject;
            ChangeShapeColor(_objectFound);
        }
    }

    //  Spawns random shape at random spawnpoint
    //  void -> void
    public void SpawnShapes()
    {
        int spawnPointIndex = UnityEngine.Random.Range(0, SpawnPoints.Length);

        int shapeIndex = Random.Range(0, Shapes.Length);

        GameObject shape = Shapes[shapeIndex];

        // Sphere instances don't require a random rotation
        if (shape.Equals("Sphere"))
        {
            Instantiate(shape, SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
        }
        else
        {
            Instantiate(shape, SpawnPoints[spawnPointIndex].position, Quaternion.Euler(Random.Range(-90, 90), Random.Range(-40, 40), Random.Range(-40, 40)));
        }
    }

    //  Spawns explosion and destroys shape object
    //  GameObject -> void
    public void DestroyShape(GameObject foundObject)
    {
        // TODO Instantiate(explosion, foundObject.transform.position, foundObject.transform.rotation);
        Destroy(foundObject);
        //Debug.Log("Destroyed object: " + foundObject.name);
    }

    // Changes object's material
    // GameObject -> void
    public void ChangeShapeColor(GameObject foundObject)
    {
        GameObject objectAlreadyFound = foundObject;

        if (foundObject.Equals("Sphere"))
        {
            foundObject.gameObject.GetComponentInParent<Renderer>().material = sphereOver;
        }

        if (foundObject.Equals("Cube"))
        {
            foundObject.gameObject.GetComponentInParent<Renderer>().material = cubeOver;
        }

        if (!foundObject.Equals(objectAlreadyFound))
        {
            if (objectAlreadyFound.Equals("Sphere"))
            {
                foundObject.gameObject.GetComponentInParent<Renderer>().material = sphereNormal;
            }

            if (objectAlreadyFound.Equals("Cube"))
            {
                foundObject.gameObject.GetComponentInParent<Renderer>().material = cubeNormal;
            }
        }

        //Color c = foundObject.gameObject.GetComponentInParent<Renderer>().material.color;
        //c.g += colorAdd;
        //c.r += colorAdd;
        //c.b += colorAdd;
        //foundObject.gameObject.GetComponentInParent<Renderer>().material.color = c;
        
    }
}
