using System;
using UnityEngine;
using UnityEngine.UI;

public class ReticleManager : MonoBehaviour {

    public Transform cam;
    public GameObject reticle;
    public RaycastManager raycastManager;
    public Color foundEnemyColor;

    private float _defaultDistance = 2f;
    private Transform _reticleTransform;
    private Vector3 _originalScale;
    private Quaternion _originalRotation;
    private RaycastHit _currentHit;
    private Renderer _reticleRend;
    private Color _defaultColor;

    public Transform GetReticleTransform() { return _reticleTransform; }


    private void Awake()
    {
        _reticleTransform = reticle.GetComponent<Transform>();
        _originalScale = _reticleTransform.localScale;
        _originalRotation = _reticleTransform.localRotation;
        _reticleRend = reticle.GetComponent<Renderer>();
        _defaultColor = _reticleRend.material.color;
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
        GameObject currentObject = raycastManager.GetCurrentFoundObject();

        if (currentObject.tag == "Enemy")
        {
            SetReticleColor(foundEnemyColor);
        }
        else
        {
            SetReticleColor();
        }
    }


    /*
     * Assigns normal of found object
     * void -> void
     */
    public void CheckNormal()
    {
        _currentHit = raycastManager.GetCurrentHit();
        SetPosition(_currentHit);
    }


    /*
     * Sets color of reticle if object found is not an enemy
     * void -> void
     */
    public void SetReticleColor()
    {
        _reticleRend.material.SetColor("_Color", _defaultColor);
    }


    /*
     * Sets color of reticle if enemy is found
     * Color -> void
     */
    public void SetReticleColor(Color color)
    {
        _reticleRend.material.SetColor("_Color", color);
    }


    private void OnEnable()
    {
        RaycastManager.OnNewObjectFound += CheckForEnemy;
        RaycastManager.OnNewNormalFound += CheckNormal;
        RaycastManager.OnNoObjectFound += SetPosition;
        RaycastManager.OnNoObjectFound += SetReticleColor;
    }

    private void OnDisable()
    {
        RaycastManager.OnNewObjectFound -= CheckForEnemy;
        RaycastManager.OnNewNormalFound -= CheckNormal;
        RaycastManager.OnNoObjectFound -= SetPosition;
        RaycastManager.OnNoObjectFound -= SetReticleColor;
    }
}