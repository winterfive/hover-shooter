using System;
using UnityEngine;
using UnityEngine.UI;

public class ReticleManager : MonoBehaviour {

    
    
    public Transform _camera;
    public float _defaultDistance = 2f;      // The default distance away from the camera the reticle is placed.
    public GameObject reticle;

    private bool _useNormal;                  // Whether the reticle should be placed parallel to a surface.    
    private Transform _reticleTransform;
    private Vector3 _originalScale;           // Since the scale of the reticle changes, the original scale needs to be stored.
    private Quaternion _originalRotation;     // Used to store the original rotation of the reticle.


    public bool UseNormal
    {
        get { return _useNormal; }
        set { _useNormal = value; }
    }


    public Transform ReticleTransform { get { return reticle.transform; } }


    private void Awake()
    {
        _reticleTransform = reticle.GetComponent<Transform>();
        _originalScale = reticle.transform.localScale;
        _originalRotation = reticle.transform.localRotation;
    }


    // This overload of SetPosition is used when the the VREyeRaycaster hasn't hit anything.
    public void SetPosition()
    {
        // Set the position of the reticle to the default distance in front of the camera.
        _reticleTransform.position = _camera.position + _camera.forward * _defaultDistance;

        // Set the scale based on the original and the distance from the camera.
        _reticleTransform.localScale = _originalScale * _defaultDistance;

        // The rotation should just be the default.
        _reticleTransform.localRotation = _originalRotation;
    }


    // This overload of SetPosition is used when the VREyeRaycaster has hit something.
    public void SetPosition(RaycastHit hit)
    {
        _reticleTransform.position = hit.point;
        _reticleTransform.localScale = _originalScale * hit.distance;

        // If the reticle should use the normal of what has been hit...
        if (_useNormal)
            // ... set it's rotation based on it's forward vector facing along the normal.
            _reticleTransform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
        else
            // However if it isn't using the normal then it's local rotation should be as it was originally.
            _reticleTransform.localRotation = _originalRotation;
    }

    private void OnEnable()
    {
        RaycastManager.OnNewObjectFound += SetPosition;
    }

    private void OnDisable()
    {
        RaycastManager.OnNewObjectFound -= SetPosition;
    }
}