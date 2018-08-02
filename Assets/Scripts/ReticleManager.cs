using System;
using UnityEngine;
using UnityEngine.UI;

public class ReticleManager : MonoBehaviour {

    public Transform cam;
    public GameObject reticle;
    public RaycastManager raycastManager;
    public Color foundEnemyColor, notFoundEnemyColor;

    private float _defaultDistance = 2f;
    private Transform _reticleTransform;
    private Vector3 _originalScale;
    private Quaternion _originalRotation;
    private RaycastHit _currentHit;

    public Transform GetReticleTransform() { return _reticleTransform; }


    private void Awake()
    {
        _reticleTransform = reticle.GetComponent<Transform>();
        _originalScale = _reticleTransform.localScale;
        _originalRotation = _reticleTransform.localRotation;
    }


    /*
     * Sets reticle to default position and scale
     * void -> void
     */
    public void SetPosition()
    {
        _reticleTransform.position = cam.position + cam.forward * _defaultDistance;
        _reticleTransform.localScale = _originalScale * _defaultDistance;
        _reticleTransform.localRotation = _originalRotation;
    }


    /*
     * Sets reticle to indicate object found
     * RaycastHit -> void
     */
    public void SetPosition(RaycastHit hit)
    {
        _reticleTransform.position = hit.point;
        _reticleTransform.localScale = _originalScale * hit.distance;
        _reticleTransform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
    }


    /*
     * Checks if current object seen by reticle is an enemy
     * void -> void
     */
    public void CheckForEnemy()
    {
        GameObject currentObject;

        if (raycastManager.GetCurrentFoundObject())
        {
            currentObject = raycastManager.GetCurrentFoundObject();
            
            if (currentObject.name == "Drone(Clone)")
            {
                ChangeReticleColor(foundEnemyColor);
            }
            
        }
        else
        {
            ChangeReticleColor(notFoundEnemyColor);
        }
    }


    /*
     * Changes color of reticle depending on type of object found
     * Color -> void
     */
    public void ChangeReticleColor(Color color)
    {
        Color reticleColor = reticle.GetComponent<Renderer>().material.color;

        reticleColor = color;
    }


    /*
     * Checks if a normal has been found, calls SetPosition()
     * void -> void
     */
    public void CheckNormalFound()
    {
        _currentHit = raycastManager.GetCurrentHit();
        SetPosition(_currentHit);
    }
 

    private void OnEnable()
    {
        RaycastManager.OnNewObjectFound += CheckForEnemy;
        RaycastManager.OnNewNormalFound += CheckNormalFound;
        RaycastManager.OnNoObjectFound += SetPosition;
    }

    private void OnDisable()
    {
        RaycastManager.OnNewObjectFound -= CheckForEnemy;
        RaycastManager.OnNewNormalFound -= CheckNormalFound;
        RaycastManager.OnNoObjectFound -= SetPosition;
    }
}