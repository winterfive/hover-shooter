using UnityEngine;

/*
* Handles reticle movement, rotation, and color
*/

public class ReticleManager : SetAsSingleton<ReticleManager> {
    
    public Transform cam;
    public GameObject reticle;
    public Color foundEnemyColor;

    private float _defaultDistance = 10f;
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
    public void SetPosition()
    {
        _reticleTransform.position = cam.position + cam.forward * _defaultDistance;
        _reticleTransform.localScale = _originalScale * _defaultDistance;
        _reticleTransform.localRotation = _originalRotation;
    }


    /*
     * Sets reticle to indicate object being looked at
     * RaycastHit -> void
     */
    public void SetPosition(RaycastHit hit)
    {
        _reticleTransform.position = hit.point;
        _reticleTransform.localScale = _originalScale * hit.distance;
        _reticleTransform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
    }


    /*
     * Calls for reticle to change color to enemyFound color
     */
     private void CheckIfEnemy()
    {
        if (_checkForEnemy.IsEnemy())
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
        _currentHit = _raycastManager.GetCurrentHit();
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
        RaycastManager.OnNewObjectFound += CheckIfEnemy;
        RaycastManager.OnNewNormalFound += CheckNormal;
        RaycastManager.OnNoObjectFound += SetPosition;
        RaycastManager.OnNoObjectFound += SetReticleColor;
    }


    private void OnDisable()
    {
        RaycastManager.OnNewObjectFound -= CheckIfEnemy;
        RaycastManager.OnNewNormalFound -= CheckNormal;
        RaycastManager.OnNoObjectFound -= SetPosition;
        RaycastManager.OnNoObjectFound -= SetReticleColor;
    }
}