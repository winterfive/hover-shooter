using UnityEngine;

public class DroneShooting : MonoBehaviour {

    public delegate void MissleFired(Transform t);
    public static event MissleFired OnMissleFired;

    private MissleValues _MVRef;
    private Transform _thisTransform;
    private float _timeBetweenMissles;
    private float _timeOfPreviousShot;
    private RaycastHit _hit;
    private int _droneRange;


    private void Awake()
    {
        GameObject missleValuesObject = GameObject.FindWithTag("ScriptManager");
        if (missleValuesObject != null)
        {
            _MVRef = missleValuesObject.GetComponent<MissleValues>();
        }
        else
        {
            Debug.Log("Cannot find MissleValues script");
        }

        _thisTransform = this.gameObject.transform;
        _timeBetweenMissles = Random.Range(_MVRef.minMissleFireRate, _MVRef.maxMissleFireRate);
        _droneRange = _MVRef.droneRange;
        _timeOfPreviousShot = 0f;
    }


    private void Update()
    {
        if (Time.time > _timeOfPreviousShot + _timeBetweenMissles)
        {
            ShootMissles();
        }
    }
    

    private void ShootMissles()
    {
        if (Physics.Raycast(_thisTransform.position, _thisTransform.forward, out _hit, _droneRange))
        {
            if (_hit.transform.tag == "Player" || _hit.transform.tag == "Shield")
            {
                if (OnMissleFired != null)
                {
                    OnMissleFired(_thisTransform);
                    _timeOfPreviousShot = Time.time;
                }
            }
        }
    }
}
