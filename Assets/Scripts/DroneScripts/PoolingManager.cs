using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : SetAsSingleton<PoolingManager>
{
    /*
     * Creates pool of GameObjects
     * GameObject, Int -> List
     */
    public List<GameObject> CreateList(GameObject prefab, int poolSize)
    {
        List<GameObject> list = new List<GameObject>();
        GameObject obj;

        for (int i = 0; i < poolSize; i++)
        {
            obj = Instantiate(prefab);
            obj.SetActive(false);
            list.Add(obj);
        }
        return list;
    }


    /*
     * Returns object form pool if an inactive object is available
     * List<GameObject> -> GameObject/null
     */
    public GameObject GetObjectFromPool(List<GameObject> l)
    {
        foreach (GameObject g in l)
        {
            if (!g.activeInHierarchy)
            {
                return g;
            }
        }
        return null;
    }


    /*
     * Returns object to its pool
     * This is the ONLY method for return in the project
     * GameObject -> void
     */
    public void ReturnToPool(GameObject go)
    {
        go.SetActive(false);
    }    
}

