using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : GenericManager<ProjectileManager> {

    public GameObject misslePrefab;
    public int misslePoolSize, missleSpeed;

    private PoolManager _poolManager;
    private List<GameObject> _missles;
    private Transform _camTransform;
    private GameObject _readyMissle;
    private Transform _readyMissleTransform;

	
	void Awake()
    {
        _poolManager = PoolManager.Instance;
        _missles = _poolManager.CreateList(misslePrefab, misslePoolSize);
        _camTransform = Camera.main.transform;
    }


    void Update()
    {
        // Add code for missle location check to deactivate/return to pool		
	}

    private void FixedUpdate()
    {
        if (_readyMissle)
        {
            Vector3 direction = Vector3.MoveTowards(_readyMissleTransform.position, _camTransform.position, missleSpeed);            
        }
    }


    public void ShootMissle(Transform gunTip)
    {
        _readyMissle = _poolManager.GetObjectFromPool(_missles);

        if (_readyMissle != null)
        {
            _readyMissle.transform.position = gunTip.position;
            _readyMissle.transform.rotation = Quaternion.LookRotation(_camTransform.position);
            _readyMissle.SetActive(true);
        }
        else
        {
            Debug.Log("All missles are currently active.");
        }
    }
}
