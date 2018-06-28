using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    public GameObject[] Shapes;
    public Transform[] SpawnPoints;
    public float spawnTime;
    public float colorAdd;

    private RaycastManager _raycastManager;
    private GameObject objectFound;
    
    //  Use this for initialization
    void Start ()
    {
        InvokeRepeating("SpawnShapes", spawnTime, spawnTime);
        _raycastManager = null;
	}

    private void Update()
    {
        if(_raycastManager.HasHitObject && _raycastManager.IfShootable)
        {
            objectFound = _raycastManager.GetObjectFound;
            ChangeShapeColor(objectFound);
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

    // Changes color of object's material via rgb values
    // GameObject -> void
    public void ChangeShapeColor(GameObject foundObject)
    {
        Color c = foundObject.GetComponent<Renderer>().material.color;
        c.g += colorAdd;
        c.r += colorAdd;
        c.b += colorAdd;
        foundObject.GetComponent<Renderer>().material.color = c;       
    }
}
