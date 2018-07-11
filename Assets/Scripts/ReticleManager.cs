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


    private bool UseNormal { get; set; }


    public Transform ReticleTransform { get { return reticle.transform; } }


    private void Awake()
    {
        _reticleTransform = reticle.GetComponent<Transform>();
        _originalScale = reticle.transform.localScale;
        _originalRotation = reticle.transform.localRotation;
    }


    // This overload of SetPosition is used when the the RaycastManager hasn't hit anything.
    // void -> void
    public void SetPosition()
    {
        // Set the position of the reticle to the default distance in front of the camera.
        _reticleTransform.position = _camera.position + _camera.forward * _defaultDistance;

        // Set the scale based on the original and the distance from the camera.
        _reticleTransform.localScale = _originalScale * _defaultDistance;

        // The rotation should just be the default.
        _reticleTransform.localRotation = _originalRotation;
    }


    // This overload of SetPosition is used when the RaycastManager has hit something.
    // RaycastHit -> void
    public void SetPosition(RaycastHit hit)
    {
        _reticleTransform.position = hit.point;
        _reticleTransform.localScale = _originalScale * hit.distance;

        // If the reticle should use the normal of what has been hit...
        if (UseNormal)
            // ... set it's rotation based on it's forward vector facing along the normal.
            _reticleTransform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
        else
            // However if it isn't using the normal then it's local rotation should be as it was originally.
            _reticleTransform.localRotation = _originalRotation;
    }

    
    // Checks current normal for null
    // Calls SetPosition
    // void -> void
    public void CheckNormalFound()
    {
        Vector3 newNormal = raycastManager.GetCurrentNormal();
        if (newNormal != null)
        {
            UseNormal = true;
            _currentHit = raycastManager.GetCurrentHit();
            SetPosition(_currentHit);
        }
        else
        {
            UseNormal = false;
            SetPosition();
        }
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