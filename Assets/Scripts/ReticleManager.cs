using System;
using UnityEngine;
using UnityEngine.UI;

public class ReticleManager : MonoBehaviour {    
    
    public Transform _camera;
    public float _defaultDistance = 2f;
    public GameObject reticle;
    public RaycastManager raycastManager;

    private Transform _reticleTransform;
    private Vector3 _originalScale;
    private Quaternion _originalRotation;
    private RaycastHit _currentHit;


    public Transform ReticleTransform { get { return reticle.transform; } }


    private void Awake()
    {
        _reticleTransform = reticle.GetComponent<Transform>();
        _originalScale = reticle.transform.localScale;
        _originalRotation = reticle.transform.localRotation;
    }


    // Used when RaycastManager hasn't hit anything.
    // void -> void
    public void SetPosition()
    {
        _reticleTransform.position = _camera.position + _camera.forward * _defaultDistance;
        // Make the reticle larger at long distance so we can see it better
        _reticleTransform.localScale = _originalScale * _defaultDistance;
        _reticleTransform.localRotation = _originalRotation;
    }


    // Used when the RaycastManager has hit something.
    // RaycastHit -> void
    public void SetPosition(RaycastHit hit)
    {
        _reticleTransform.position = hit.point;
        _reticleTransform.localScale = _originalScale * hit.distance;
        _reticleTransform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);        
    }

    
    // Calls SetPosition
    // void -> void
    public void CheckNormalFound()
    {
        _currentHit = raycastManager.GetCurrentHit();
        SetPosition(_currentHit);        
    }
 

    private void OnEnable()
    {
        RaycastManager.OnNewNormalFound += CheckNormalFound;
    }

    private void OnDisable()
    {
        RaycastManager.OnNewNormalFound -= CheckNormalFound;
    }
}