using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EffectsManager : SetAsSingleton<EffectsManager> {

    public Color[] destructionColors;
    public float waitBetweenColors;

    private GameObject _shotObject;
    private RaycastManager _raycastManager;
    private PoolingManager _poolingManager;

    
    void Awake ()
    {
        _raycastManager = RaycastManager.Instance;
        _poolingManager = PoolingManager.Instance;
	}


    public void TerminateEnemy()
    {
        _shotObject = _raycastManager.GetCurrentFoundObject();
        StopEnemyActions();
        StartCoroutine(FadeEffect());
    }


    private IEnumerator FadeEffect()
    {
        Renderer[] components = _shotObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer r in components)
        {
            r.material.mainTexture = null;
        }

        foreach (Color c in destructionColors)
        {
            foreach (Renderer r in components)
            {
                r.material.color = c;
            }

            yield return new WaitForSeconds(waitBetweenColors);
        }

        _poolingManager.ReturnToPool(_shotObject);
    }


    private void StopEnemyActions()
    {
        // TODO AttackDroneActions is null
        _shotObject.GetComponent<AttackDroneActions>().IsShot = true;
    }
}
