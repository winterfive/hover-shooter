using System.Collections;
using UnityEngine;

public class EnemyShootManager : MonoBehaviour {

    private Vector3 _camPosition;
    private Transform _gunTipTransform;
    private RaycastHit _hit;

    private void Start()
    {
        _camPosition = Camera.main.gameObject.transform.position;
        _gunTipTransform = this.gameObject.transform;
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
