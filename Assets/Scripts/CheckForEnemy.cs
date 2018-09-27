using UnityEngine;

/*
 * Checks player raycast hit for enemy
 */
public class CheckForEnemy : GenericManager<CheckForEnemy> {

    public delegate void EnemySpotted();
    public static event EnemySpotted OnEnemySpotted;

    private RaycastManager _raycastManager;

	// Use this for initialization
	void Start ()
    {
        _raycastManager = RaycastManager.Instance;		
	}


    /*
     * Checks if current object seen by reticle is an enemy
     * void -> void
     */
    public void CheckFoundForEnemy()
    {
        GameObject currentObject = _raycastManager.GetCurrentFoundObject();

        if (currentObject.tag == "Enemy" || currentObject.tag == "Turret" || currentObject.tag == "Glow")
        {
            if (OnEnemySpotted != null)
            {
                OnEnemySpotted();
            }
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
