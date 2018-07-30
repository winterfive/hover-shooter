using System;
using UnityEngine;
using UnityEngine.UI;

public class ReticleManager : MonoBehaviour {    
    
    public Transform _camera;
    public float _defaultDistance = 2f;
    public GameObject reticle;
    public RaycastManager raycastManager;
    public Color targetAquiredColor, noTargetColor;

    private Transform _reticleTransform;
    private Vector3 _originalScale;
    private Quaternion _originalRotation;
    private Color _reticleColor;
    private RaycastHit _currentHit;
    private GameObject _currentObject;

    public Transform ReticleTransform { get { return reticle.transform; } }


    private void Awake()
    {
        _reticleTransform = reticle.GetComponent<Transform>();
        _originalScale = reticle.transform.localScale;
        _originalRotation = reticle.transform.localRotation;
        _reticleColor = reticle.GetComponent<Renderer>().material.color;
    }


    /*
     * Sets reticle to default position and scale
     * void -> void
     */
    public void SetPosition()
    {
        _reticleTransform.position = _camera.position + _camera.forward * _defaultDistance;
        _reticleTransform.localScale = _originalScale * _defaultDistance;
        _reticleTransform.localRotation = _originalRotation;
        _reticleColor = noTargetColor;
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
     * Changes color of reticle if object found is enemy 
     * gameObject -> void
     */
    public void ChangeReticleColor(GameObject go)
    {
        _reticleColor = targetAquiredColor;
    }


    /*
     * Sets color of reticle if object is not enemy 
     * gameObject -> void
     */
    public void RevertReticleColor(GameObject go)
    {
        _reticleColor = noTargetColor;
    }


    /*
     * Checks if current object seen by reticle is an enemy
     * void -> void
     */
    public void CheckObjectFound()
    {
        _currentObject = raycastManager.GetCurrentFoundObject();

        if(_currentObject.tag == "Drone")
        {
            ChangeReticleColor(_currentObject);
        }
        else
        {
            RevertReticleColor(_currentObject);
        }
    }

    
   /*
    * Listens for RaycastManager updates
    * void -> void
    */ 
    public void CheckNormalFound()
    {
        _currentHit = raycastManager.GetCurrentHit();
        SetPosition(_currentHit);        
    }
 

    private void OnEnable()
    {
        RaycastManager.OnNewNormalFound += CheckNormalFound;
        RaycastManager.OnNewObjectFound += CheckObjectFound;
    }

    private void OnDisable()
    {
        RaycastManager.OnNewNormalFound -= CheckNormalFound;
        RaycastManager.OnNewObjectFound -= CheckObjectFound;
    }
}