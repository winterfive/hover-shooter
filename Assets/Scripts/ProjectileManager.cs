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
    private Transform _guntTipTransform;

	
	void Awake ()
    {
        _poolManager = PoolManager.Instance;
        _missles = _poolManager.CreateList(misslePrefab, misslePoolSize);
    }


    void Update ()
    {
        // Add code for missle location check to deactivate/return to pool		
	}

    private void FixedUpdate()
    {
       if (_readyMissle != null)
       {
            _readyMissle.GetComponent<Rigidbody>().velocity = (_guntTipTransform.transform.position - _camTransform.position) * missleSpeed;
       }
    }


    public void ShootMissle(Transform t)
    {
        _readyMissle = _poolManager.GetObjectFromPool(_missles);

        if (_readyMissle != null)
        {
            _readyMissle.transform.position = t.position;
            _readyMissle.transform.rotation = Quaternion.LookRotation(_camTransform.position);
            _readyMissle.SetActive(true);
            
        }
        else
        {
            Debug.Log("All missles are currently active.");
        }
    }

    private void OnEnable()
    {
        DroneActions.TargetAcquiredHandler += ShootMissle;
    }
}
