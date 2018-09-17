using System.Collections.Generic;
using UnityEngine;


public class AttackDrone : Drone, ILookAt<Transform>
{
    public Transform[] spawnPoints;
    public Transform[] endPoints;

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


    public new Vector3 CreateRandomPosition()
    {
        Vector3 randomPosition;

        randomPosition.x = Random.Range(xMin, xMax);
        randomPosition.y = Random.Range(yMin, yMax);
        randomPosition.z = Random.Range(zMin, zMax);

        return randomPosition;
    }


    public new Vector3 SelectLastPosition()
    {
        int index = Random.Range(0, endPoints.Length);
        Vector3 endPosition = endPoints[index].position;
        return endPosition;
    }
}
