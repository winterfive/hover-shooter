using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneShooting : MonoBehaviour {

    public GameObject missle;
    public PoolManager poolManager;
    public int poolSize;
    public float missleSpeed;

    private Vector3 _camPosition;
    private Transform _gunTipTransform;
    private List<GameObject> _missles;
    private RaycastHit _hit;


    private void Start()
    {
        _camPosition = Camera.main.gameObject.transform.position;        
        _missles = poolManager.CreateList(missle, poolSize);
    }


    public void ShootAtPlayer()
    {
        GameObject readyMissle = GetObjectFromPool();
        Rigidbody rb = readyMissle.GetComponent<Rigidbody>();

        readyMissle.transform.position = _gunTipTransform.position;
        rb.velocity = transform.forward * missleSpeed;

        Debug.Log("ShootAtPlayer has been called");
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
        return null;
    }

    // TODO Listens for DroneActions ShootAtPlayer call
}
