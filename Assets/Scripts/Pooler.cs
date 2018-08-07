using UnityEngine;
using System.Collections.Generic;

public class Pooler<T> : MonoBehaviour where T : MonoBehaviour
{
    public int poolSize;
    public T prefab;

    private List<T> _availableList;
    private List<T> _usedList;


    void Awake()
    {
        _availableList = new List<T>(poolSize);
        _availableList = new List<T>(poolSize);

        for (int i = 0; i < poolSize; i++)
        {
            var pooledObj = Instantiate(prefab);
            pooledObj.gameObject.SetActive(false);
            _availableList.Add(pooledObj);
        }
    }


    public T GetPooledObject()
    {
        int availableCount = _availableList.Count;
        if (availableCount == 0)
        {
            return null;
        }

        T obj = _availableList[availableCount - 1];
        _availableList.Remove(obj);
        _usedList.Add(obj);
        return obj;
    }


    public void ReturnPooledObject(T obj)
    {
        Debug.Assert(_usedList.Contains(obj));

        _usedList.Remove(obj);
        _availableList.Add(obj);
        obj.gameObject.SetActive(false);
    }
}