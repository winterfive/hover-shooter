using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneShooting : MonoBehaviour {

    public GameObject missle;
    public PoolManager poolManager;
    public int poolSize;

    private Vector3 _camPosition;
    private Transform _gunTipTransform;
    private List<GameObject> _missles;
    private RaycastHit _hit;

    private void Start()
    {
        _camPosition = Camera.main.gameObject.transform.position;
        _gunTipTransform = this.gameObject.transform;
        _missles = poolManager.CreateList(missle, poolSize);
    }

    private void Update()
    {
        if (Physics.Raycast(_gunTipTransform.position, _gunTipTransform.forward, out _hit, 125))
        {
            if (_hit.transform.tag == "Player")
            {
                ShootAtPlayer();
            }
        }
    }

    private void ShootAtPlayer()
    {

    }
}
