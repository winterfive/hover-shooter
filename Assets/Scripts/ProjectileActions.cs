using System;
using UnityEngine;

public class ProjectileActions : MonoBehaviour
{
    public int missleSpeed;
    public delegate void PlayerHit();
    public static PlayerHit OnPlayerHit;
    public delegate void ShieldHit();
    public static ShieldHit OnShieldHit;
    
    private GameObject _missle;
    private Transform _camTransform;
    private Transform _target;


    // Use this for initialization
    void Awake()
    {
        _camTransform = Camera.main.transform;
        _missle = this.gameObject;
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (OnPlayerHit != null)
            {
                OnPlayerHit();
            }
        }

        if (col.gameObject.tag == "Shield")
        {
            if (OnShieldHit != null)
            {
                OnShieldHit();
            }
        }

        _missle.SetActive(false);
    }


    void FixedUpdate()
    {
        if (_missle.activeInHierarchy)
        {
            MissleFly();
        }       
    }


    /*
     * Checks if missle distance to see if it should continue flying
     * void -> void
     */
    private void MissleFly()
    {
        if (Vector3.Distance(_missle.transform.position, _camTransform.position) > 0.5)
        {
            float step = missleSpeed * Time.deltaTime;
            _missle.transform.position = Vector3.MoveTowards(_missle.transform.position, _camTransform.position, step);
            _missle.transform.LookAt(_camTransform);
        }
    }
}
