﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {


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
}