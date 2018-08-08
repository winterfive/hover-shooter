using UnityEngine;


public class DestroyObjects : MonoBehaviour
{
    public RaycastManager raycastManager;
    public ParticleSystem droneExplosion;


    /*
     * Activates all actions required when drone is shot by player
     * void -> void
     */
    public void DestroyEnemy()
    {
        GameObject currentObject = raycastManager.GetCurrentFoundObject();
        Debug.Log("current object is: " + currentObject.tag);

        if (currentObject.tag == "Enemy")
        {
            Instantiate(droneExplosion, currentObject.transform);
            currentObject.SetActive(false);
        }

        // glow lerp gets fast for a second, then stops
        // Call explosion script in EffectsManager
        // stop shooting script, shooting = false
        // begin drone hit anim (drone wavers and tilts)
        // turn on gravity for drone
        // Call smoke script in EffectsManager
        // Drone falls through floor slowly


    }

    private void OnEnable()
    {
        GameManager.OnShotEnemy += DestroyEnemy;
    }

    private void OnDisable()
    {
        GameManager.OnShotEnemy -= DestroyEnemy;
    }
}   