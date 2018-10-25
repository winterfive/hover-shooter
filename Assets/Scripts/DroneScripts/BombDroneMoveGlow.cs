using UnityEngine;
using UnityEngine.AI;

public class BombDroneMoveGlow : DroneActions
{
    private Vector3 _camPosition;
    private GameObject _this;
    private NavMeshAgent _agent;
    private BombDroneValues _BDVRef;


    void Awake()
    {
        GameObject bombDroneValuesObject = GameObject.FindWithTag("ScriptManager");
        if (bombDroneValuesObject != null)
        {
            _BDVRef = bombDroneValuesObject.GetComponent<BombDroneValues>();
        }
        else
        {
            Debug.Log("Cannot find BombDroneValues script");
        }

        _camPosition = _BDVRef.cam.transform.position;
        _this = this.gameObject;
        _agent = _this.GetComponent<NavMeshAgent>();
    }

    // TODO BombDrone not moving or mesh appearing when this script is on the prefab
    // When it's removed from prefab, prefab is spawned and doesnt move.  


    private void OnEnable()
    {
        if (_agent.isOnNavMesh && _agent.isActiveAndEnabled)
        {
            GoToPlayer();
            _agent.speed = Random.Range(_BDVRef.minSpeed, _BDVRef.maxSpeed);
            _agent.baseOffset = Random.Range(_BDVRef.altitudeMin, _BDVRef.altitudeMax);
        }
        else
        {
            Debug.Log("Bomb drone returned to pool (not on navMesh)");
            // Return to pool
            _this.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PlayerTrigger")
        {
            // Bomb drone got within range of player and detonated
            Debug.Log("Bomb drone reached target");
            // Call Effects manager for explosion effect normal or agaisnt shield, pass the go

            _this.SetActive(false);
        }        
    }


    private void GoToPlayer()
    {
        Vector3 endPoint = _camPosition;
        endPoint.y = _agent.baseOffset;
        _agent.destination = endPoint;
    }
}
