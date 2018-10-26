using System;
using UnityEngine;

public class MissleActions : MonoBehaviour
{
    /*
     * Class handles all missle actions as they pertain to the 
     * individual missle (flight, what it hits).
     */

    public delegate void PlayerHit();
    public static PlayerHit OnPlayerHit;
    public delegate void ShieldHit();
    public static ShieldHit OnShieldHit;

    private MissleValues _MVRef;
    private GameObject _missle;
    private Transform _camTransform;
    private int _missleSpeed;


    // Use this for initialization
    void Awake()
    {
        GameObject missleValuesObjectRef = GameObject.FindWithTag("ScriptManager");
        if (missleValuesObjectRef != null)
        {
            _MVRef = missleValuesObjectRef.GetComponent<MissleValues>();
        }
        else
        {
            Debug.Log("Cannot find MissleValues script");
        }

        _camTransform = Camera.main.transform;
        _missle = this.gameObject;
        _missleSpeed = _MVRef.missleSpeed;        
    }


    void OnTriggerEnter(Collider col)
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


    void Update()
    {
        MissleFly();       
    }


    /*
     * Checks missle distance to see if it should continue flying
     * void -> void
     */
    private void MissleFly()
    {
        Debug.Log("got to here, missle flying");
        if (Vector3.Distance(_missle.transform.position, _camTransform.position) > 0.5)
        {
            float step = _missleSpeed * Time.deltaTime;
            _missle.transform.position = Vector3.MoveTowards(_missle.transform.position, _camTransform.position, step);
            _missle.transform.LookAt(_camTransform);
        }
    }
}
