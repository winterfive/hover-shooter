using UnityEngine;
using UnityEngine.AI;

public class BombDrone : MonoBehaviour
{
    private Vector3 _camPosition;
    private GameObject _this;
    private NavMeshAgent _agent;
    private BombDroneManager _bombDroneManagerReference;
    private Renderer _glowRenderer;
    private Color _defaultGlowColor;
    private Color _otherGlowColor;
    private float _glowSpeed;
    private bool _isAlive;

    // TODO Every drone will need a stopEverything method based on a bool
    // so that it will stop moving/glowing once it's been shot


    void Awake()
    {
        GameObject bombDroneManagerObject = GameObject.FindWithTag("ScriptManager");
        if (bombDroneManagerObject != null)
        {
            _bombDroneManagerReference = bombDroneManagerObject.GetComponent<BombDroneManager>();            
        }
        else
        {
            Debug.Log("Cannot find AttackDroneManager script");
            // Return to pool
            _bombDroneManagerReference.SetToInactive(_this);
        }

        _camPosition = Camera.main.transform.position;
        _this = this.gameObject;
        _agent = _this.GetComponent<NavMeshAgent>();
        _glowRenderer = FindChildWithTag("Glow").GetComponent<Renderer>();
        _defaultGlowColor = _glowRenderer.material.color;
        _glowSpeed = _bombDroneManagerReference.glowSpeed;
        _otherGlowColor = _bombDroneManagerReference.secondGlowColor;
        _isAlive = true;
    }


    private void Start()
    {
        if (_agent.isOnNavMesh && _agent.isActiveAndEnabled)
        {
            _agent.speed = _bombDroneManagerReference.GetRandomSpeed();
            _agent.baseOffset = _bombDroneManagerReference.GetRandomOffset();
            _agent.SetDestination(_camPosition);
        }
        else
        {
            Debug.Log("Attack drone returned to pool (not on navMesh)");
            // Return to pool
            _bombDroneManagerReference.SetToInactive(_this);
        }
    }


    void Update()
    {
        LerpColor();

        // Check if drone is close to mid point or end point
        if (Time.frameCount % 30 == 0)
        {
            // TODO 
            //if (drone enters bombdronetarget)
            // Explode();
        }
    }


    /*
     * Finds child transform with tag
     * void -> transform
     */
    public Transform FindChildWithTag(string a)
    {
        Transform[] components = _this.GetComponentsInChildren<Transform>();

        foreach (Transform t in components)
        {
            if (t.gameObject.CompareTag(a))
            {
                return t;
            }
        }
        return null;
    }


    /*
     * Pingpongs color steadily from one color to another
     * void -> void
     */
    private void LerpColor()
    {
        if (_glowRenderer)
        {
            float pingpong = Mathf.PingPong(Time.time * _glowSpeed, 1.0f);
            _glowRenderer.material.color = Color.Lerp(_defaultGlowColor, _otherGlowColor, pingpong);
        }
    }
}
