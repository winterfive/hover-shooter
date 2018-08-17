using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneShooting : MonoBehaviour {

    public float missleSpeed;
    public int poolSize;
    public GameObject prefab;
    
    private Vector3 _camPosition;
    private List<GameObject> _missles;
    private RaycastHit _hit;
    private Transform _localTransform;
    private PoolManager _poolManager;


    private void Start()
    {
        _camPosition = Camera.main.gameObject.transform.position;
        _localTransform = this.gameObject.transform;
        _poolManager = PoolManager.Instance;
        _missles = _poolManager.CreateList(prefab, poolSize);
    }


    private void Update()
    {
        // TODO add time spacing to this so drones are not contstantly shooting
        if (Physics.Raycast(_localTransform.position, _localTransform.up, out _hit, 1000))
        {
            Debug.DrawRay(_localTransform.position, _localTransform.up);
            if (_hit.transform.tag == "Player")
            {
                Debug.Log("Missle fired");
                FireMissle();
            }
        }
    }


    public void FireMissle()
    {
        GameObject readyMissle = GetObjectFromPool();

        if (readyMissle != null)
        {
            readyMissle.transform.position = _localTransform.position;
            readyMissle.SetActive(true);
            readyMissle.GetComponent<Rigidbody>().velocity = transform.forward * missleSpeed;
        }        

        Debug.Log("FireMissle has been called");
        //TODO if missles hits player collider, event player hit
    }


    GameObject GetObjectFromPool()
    {
        foreach (GameObject missle in _missles)
        {
            if (!missle.activeInHierarchy)
            {
                return missle;
            }
        }

        Debug.Log("No more missles available");
        return null;
    }
}
