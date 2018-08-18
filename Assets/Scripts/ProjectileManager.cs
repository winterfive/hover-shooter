using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : GenericManager<ProjectileManager> {

    public GameObject misslePrefab;
    public int misslePoolSize, missleSpeed;

    private PoolManager _poolManager;
    private List<GameObject> _missles;
    private Transform _camTransform;

	
	void Awake ()
    {
        _poolManager = PoolManager.Instance;
        _missles = _poolManager.CreateList(misslePrefab, misslePoolSize);
    }
	
	
	void Update ()
    {
        // Add code for missle location check to deactivate/return to pool		
	}


    public void ShootMissle(Transform t)
    {
        GameObject readyMissle = _poolManager.GetObjectFromPool(_missles);

        if (readyMissle != null)
        {
            readyMissle.transform.position = t.position;
            readyMissle.transform.rotation = Quaternion.LookRotation(_camTransform.position);
            readyMissle.SetActive(true);
            readyMissle.GetComponent<Rigidbody>().velocity = (t.transform.position - _camTransform.position) * missleSpeed;
        }
        else
        {
            Debug.Log("All missles are currently active.");
        }
    }
}
