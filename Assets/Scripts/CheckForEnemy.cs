using UnityEngine;

/*
 * Checks player raycast hit for enemy
 */
public class CheckForEnemy : SetAsSingleton<CheckForEnemy> {
    
    private RaycastManager _raycastManager;
    private bool _isEnemy;

    public bool IsEnemy() { return _isEnemy; }


	// Use this for initialization
	void Start ()
    {
        _raycastManager = RaycastManager.Instance;
        _isEnemy = false;
	}


    /*
     * Checks if current object seen by reticle is an enemy
     * void -> void
     */
    public void CheckFoundForEnemy()
    {
        GameObject currentObject = _raycastManager.GetCurrentFoundObject();

        currentObject = currentObject.transform.parent.gameObject;

        if (currentObject.CompareTag("Enemy"))
        {
            _isEnemy = true;
        }
        else
        {
            _isEnemy = false;
        }
    }


    private void OnEnable()
    {
        RaycastManager.OnNewObjectFound += CheckFoundForEnemy;
    }


    private void OnDisable()
    {
        RaycastManager.OnNewObjectFound -= CheckFoundForEnemy;
    }


}
