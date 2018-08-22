using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : GenericManager<ProjectileManager> {

    public GameObject misslePrefab;
    public int misslePoolSize;

    private PoolManager _poolManager;
    private List<GameObject> _missles;

	
	void Awake()
    {
        _poolManager = PoolManager.Instance;
        _missles = _poolManager.CreateList(misslePrefab, misslePoolSize);
    }
}
