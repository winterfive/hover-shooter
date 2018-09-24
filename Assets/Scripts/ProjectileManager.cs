using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : GenericManager<ProjectileManager> {

    public GameObject misslePrefab;
    public int misslePoolSize;

    private PoolManager _poolManager;
    private List<GameObject> _missles;
    private GameObject _missle;

    void Awake()
    {
        _poolManager = PoolManager.Instance;
        _missles = _poolManager.CreateList(misslePrefab, misslePoolSize);
    }


    public void ShootMissle(Transform t)
    {
        _missle = _poolManager.GetObjectFromPool(_missles);

        if (_missle)
        {
            _missle.transform.position = t.position;
            _missle.transform.rotation = t.rotation;
            _missle.SetActive(true);
        }
        else
        {
            // Debug.Log("No missles are available right now");
        }
    }

    private void OnEnable()
    {
        //DroneActions.OnMissleFired += ShootMissle;
    }

    private void OnDisable()
    {
        //DroneActions.OnMissleFired -= ShootMissle;
    }
}
