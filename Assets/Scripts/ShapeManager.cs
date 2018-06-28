using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    public GameObject[] Shapes;
    public Transform[] SpawnPoints;
    public float spawnTime;
    public Material normalMaterial;
    public Material onGazeMaterial;

    private RaycastManager _raycastManager;
    

    //  Use this for initialization
    void Start ()
    {
        InvokeRepeating("SpawnShapes", spawnTime, spawnTime);
	}

    //  Spawns random shape at random spawnpoint
	//  void -> void
    public void SpawnShapes()
    {
        int spawnPointIndex = UnityEngine.Random.Range(0, SpawnPoints.Length);

        int shapeIndex = Random.Range(0, Shapes.Length);

        GameObject shape = Shapes[shapeIndex];

        Instantiate(shape, SpawnPoints[spawnPointIndex].position, Quaternion.Euler(Random.Range(-90, 90), Random.Range(-40, 40), Random.Range(-40, 40)));
    }

    //  Spawns explosion and destroys shape object
    //  GameObject -> void
    public void DestroyShape(GameObject foundObject)
    {
        // TODO Instantiate(explosion, foundObject.transform.position, foundObject.transform.rotation);
        Destroy(foundObject);
        //Debug.Log("Destroyed object: " + foundObject.name);
    }

    // Changes color of object's material
    // GameObject -> void
    public void ChangeColor(GameObject foundObject)
    {
        //if(Shape is cube)
        //{
        //    AudioClipLoadType cube over material
        //}
        //if(Shape is sphere)
        //{
        //    AudioClipLoadType sphere over material
        //}
        //foundObject.GetComponent<Renderer>().material = onGazeMaterial;
    }
}
