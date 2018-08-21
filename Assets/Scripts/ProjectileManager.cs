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

	
	void Awake()
    {
        _poolManager = PoolManager.Instance;
        _missles = _poolManager.CreateList(misslePrefab, misslePoolSize);
        _camTransform = Camera.main.transform;
    }


    void Update()
    {
        // Add code for missle location check to deactivate/return to pool

        if (_readyMissle != null)
        {
            if (Vector3.Distance(_readyMissle.transform.position, _camTransform.position) > 0.5)
            {
                float step = missleSpeed * Time.deltaTime;
                _readyMissle.transform.position = Vector3.MoveTowards(_readyMissle.transform.position, _camTransform.position, step);
                Debug.Log("step is: " + step);
            }
            else
            {
                _readyMissle.SetActive(false);
            }
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
