using System;
using UnityEngine;

public class AttackDrone : Drone, ILookAt<Transform>
{
    private Transform _camTransform;
    private Transform _thisTransform;

    void Awake()
    {
        _camTransform = Camera.main.transform;
        _thisTransform = this.gameObject.transform;
    }

    //void Start()
    //{
    //    GameObject attackDroneManagerObject = GameObject.FindWithTag("ScriptManager");
    //    if (attackDroneManagerObject != null)
    //    {
    //        _attackDroneManagerReference = attackDroneManagerObject.GetComponent<DroneManager>();
    //    }

    //    if (_droneManagerReference == null)
    //    {
    //        Debug.Log("Cannot find DroneManager script");
    //    }
    //}

    void Update()
    {
        LookAt(_camTransform);
    }


    /*
     * This implementation only rotates on the y axis
     * Transform -> void
     */
    public void LookAt(Transform t)
    {
        if (_thisTransform)
        {
            Vector3 newVector = new Vector3(_thisTransform.transform.position.x - t.position.x,
                                            0f,
                                            _thisTransform.transform.position.z - t.position.z);

            _thisTransform.transform.rotation = Quaternion.LookRotation(newVector);
        }
    }

    void ILookAt<Transform>.LookAt(Transform transformBeingLookedAt)
    {
        throw new NotImplementedException();
    }
}
