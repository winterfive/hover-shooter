using UnityEngine;

public class ProjectileActions : MonoBehaviour
{
    public int missleSpeed;
    public Transform[] targetPoints;
    
    private GameObject _missle;
    private Transform _camTransform;
    private Transform _target;


    // Use this for initialization
    void Awake()
    {
        _camTransform = Camera.main.transform;
        _missle = this.gameObject;
    }

    void Update()
    {
        if (Vector3.Distance(_missle.transform.position, _camTransform.position) > 0.5)
        {
            float step = missleSpeed * Time.deltaTime;
            _missle.transform.position = Vector3.MoveTowards(_missle.transform.position, _camTransform.position, step);
            _missle.transform.LookAt(_camTransform);
        }
        else
        {
            // Missle has hit Player
            // Call playerHit event
            _missle.SetActive(false);
        }
    }


    
}
