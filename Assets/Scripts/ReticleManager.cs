using UnityEngine;

/*
* Handles reticle movement, rotation, and color
*/

public class ReticleManager : SetAsSingleton<ReticleManager> {
    
    public Transform cam;
    public GameObject reticle;
    public Color foundEnemyColor;
    
    private Transform _reticleTransform;
    private Vector3 _originalScale;
    private Quaternion _originalRotation;
    private RaycastHit _currentHit;
    private Renderer _reticleRend;
    private Color _defaultColor;
    private RaycastManager _raycastManager;
    private CheckForEnemy _checkForEnemy;

    public Transform GetReticleTransform() { return _reticleTransform; }


    private void Awake()
    {
        _reticleTransform = reticle.GetComponent<Transform>();
        _originalScale = _reticleTransform.localScale;
        _originalRotation = _reticleTransform.localRotation;
        _reticleRend = reticle.GetComponent<Renderer>();
        _defaultColor = _reticleRend.material.color;
        _raycastManager = RaycastManager.Instance;
        _checkForEnemy = CheckForEnemy.Instance;
    }


    /*
     * Sets reticle to default position and scale
     * void -> void
     */
    public void SetReticleToDefault()
    {
        _reticleTransform.position = cam.position + cam.forward;
        _reticleTransform.localScale = _originalScale;
        _reticleTransform.localRotation = _originalRotation;
        _reticleRend.material.color = _defaultColor;
    }


    /*
     * Sets reticle to found object's position and rotation
     * RaycastHit -> void
     */
    public void SetReticleToObject()
    {
        _currentHit = _raycastManager.GetCurrentHit();
        _reticleTransform.position = _currentHit.point;
        _reticleTransform.localScale = _originalScale * _currentHit.distance;
        _reticleTransform.rotation = Quaternion.FromToRotation(Vector3.forward, _currentHit.normal);
    }


    /*
     * Calls for enemyCheck to see what color the reticle should be
     * void -> void
     */
     private void CheckIfEnemy()
    {
        if (_checkForEnemy.IsEnemy())
        {
            SetReticleColor(foundEnemyColor);
        }
        else
        {
            SetReticleColor(_defaultColor);
        }
    }


    /*
     * Sets color of reticle
     * Color -> void
     */
    public void SetReticleColor(Color color)
    {
        _reticleRend.material.SetColor("_Color", color);
    }


    private void OnEnable()
    {
        RaycastManager.OnNewObjectFound += CheckIfEnemy;
        RaycastManager.OnNewNormalFound += SetReticleToObject;
        RaycastManager.OnNoObjectFound += SetReticleToDefault;
    }


    private void OnDisable()
    {
        RaycastManager.OnNewObjectFound -= CheckIfEnemy;
        RaycastManager.OnNewNormalFound -= SetReticleToObject;
        RaycastManager.OnNoObjectFound -= SetReticleToDefault;
    }
}