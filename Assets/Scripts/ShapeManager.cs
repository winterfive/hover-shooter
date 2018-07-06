﻿using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    public GameObject[] Shapes;
    public Transform[] SpawnPoints;
    public float startTime = 2.0f;
    public float spawnTime;
    public RaycastManager raycastManager;
    //public Material cubeNormal;
    //public Material cubeOver;
    //public Material sphereNormal;
    //public Material sphereOver;

    private GameObject _currentGameObject;
    
    
    //  Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnShapes", spawnTime, spawnTime);
	}

    private void Update()
    {
        
    }

    //  Spawns random shape at random spawnpoint
    //  void -> void
    public void SpawnShapes()
    {
        int spawnPointIndex = Random.Range(0, SpawnPoints.Length);

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
 
    // TODO Get the object from Raycaster if event is called
    // TODO If the current object is shootable, change it's color back to normal
    // TODO Change the new object's color to onGaze if it's shootable
    // TODO Store the ne wobjecy as the current object
    // TODO ShapeManager checks for shootable


    //  Spawns explosion and destroys shape object
    //  GameObject -> void
    public void DestroyShape(GameObject foundObject)
    {
        // TODO Instantiate(explosion, foundObject.transform.position, foundObject.transform.rotation);
        Destroy(foundObject);
        //Debug.Log("Destroyed object: " + foundObject.name);
    }


    // Checks parent object of object found for "Shootable" tag
    // void -> void
    public void CheckForShootable()
    {
        _currentGameObject = raycastManager.GetCurrentFoundObject();

        if (_currentGameObject.tag == "Shootable")
        {
            ApplyNormalColor(_currentGameObject);
            // change color of previous object back to normal (if it's shootable)
        }
    }


    private void OnEnable()
    {
        EventManager.OnNewObjectFound += CheckForShootable;
    }

    private void OnDisable()
    {
        EventManager.OnNewObjectFound -= CheckForShootable;
    }

    public void ApplyOverColor(GameObject go)
    {

    }

    public void ApplyNormalColor(GameObject go)
    {

    }
}

