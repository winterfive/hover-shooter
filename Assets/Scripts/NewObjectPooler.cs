using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewObjectPooler : MonoBehaviour {

    public static NewObjectPooler currentPooler;
    public GameObject pooledObject;
    public int poolSize;
    public bool willGrow = true;

    List<GameObject> pooledObjects;

    void Awake()
    {
        currentPooler = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();

        for(int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (willGrow)
        {
            GameObject obj = Instantiate(pooledObject);
            pooledObject.SetActive(true);
            return obj;
        }

        return null;
    }
}
