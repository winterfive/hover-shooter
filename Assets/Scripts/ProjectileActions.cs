using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileActions : MonoBehaviour {

    public int missleSpeed;

    private List<GameObject> _missles;
    private GameObject _missle;
    private Transform _camTransform;
    private ProjectileManager _projectileManagerReference;


	// Use this for initialization
	void Awake ()
    {
        _camTransform = Camera.main.transform;
        _missle = this.gameObject;
    }

    private void Start()
    {
        GameObject projectileManagerObject = GameObject.FindWithTag("ScriptManager");
        if (projectileManagerObject != null)
        {
            _projectileManagerReference = projectileManagerObject.GetComponent<ProjectileManager>();
        }

        if (_projectileManagerReference == null)
        {
            Debug.Log("Cannot find projectileManager script");
        }
    }

    void Update()
    {
        // if (missle has hit player)
        {
            // set active to false
            // call PLayerHit()
        }

        if (Vector3.Distance(_missle.transform.position, _camTransform.position) > 0.5)
            {
                float step = missleSpeed * Time.deltaTime;
                _missle.transform.position = Vector3.MoveTowards(_missle.transform.position, _camTransform.position, step);
            }
            else
            {
                _missle.SetActive(false);
            }
        }
    }


    public void ShootMissle(Transform gunTip)
    {
        _missle = _poolManager.GetObjectFromPool(_missles);

        if (_missle != null)
        {
            _missle.transform.position = gunTip.position;
            _missle.transform.rotation = Quaternion.LookRotation(_camTransform.position);
            _missle.SetActive(true);
        }
        else
        {
            Debug.Log("All missles are currently active.");
        }
    }
}
