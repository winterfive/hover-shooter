using System;
using UnityEngine;
using UnityEngine.UI;

public class ReticleManager : MonoBehaviour {    
    
    public Transform cam;    
    public GameObject reticle;
    public RaycastManager raycastManager;
    public Color targetAquiredColor, noTargetColor;

    private float _defaultDistance = 2f;
    private Transform _reticleTransform;
    private Vector3 _originalScale;
    private Quaternion _originalRotation;
    private Color _reticleColor;
    private RaycastHit _currentHit;
    private GameObject _currentObject;

    public Transform ReticleTransform { get { return _reticleTransform; } } // Needed for destroy & explosion, later


    private void Awake()
    {
        _reticleTransform = reticle.GetComponent<Transform>();
        _originalScale = _reticleTransform.localScale;
        _originalRotation = _reticleTransform.localRotation;
        _reticleColor = _reticleTransform.GetComponent<Renderer>().material.color;
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
     * Changes color of reticle depending on type of object found
     * void -> void
     */
    public void ChangeReticleColor(Color color)
    {
        _reticleColor = color;
    }


    /*
     * Checks if current object seen by reticle is an enemy
     * void -> void
     */
    public void CheckForEnemy()
    {
        _currentObject = raycastManager.GetCurrentFoundObject();

        if (_currentObject.tag == "Drone")
        {
            ChangeReticleColor(targetAquiredColor);
        }
        else
        {
            ChangeReticleColor(noTargetColor);
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
        // TODO When raycast finds the skybox (nothing), the reticle rotation gets stuck
        // TODO SetPosition() isn't being called
        // TODO In shape shooter, soemthing always got hit, here we can hit the skybox!
    }
 

    private void OnEnable()
    {
        RaycastManager.OnNewObjectFound += CheckForEnemy;
        RaycastManager.OnNewNormalFound += CheckNormalFound;        
    }

    private void OnDisable()
    {
        RaycastManager.OnNewObjectFound -= CheckForEnemy;
        RaycastManager.OnNewNormalFound -= CheckNormalFound;

    }
}