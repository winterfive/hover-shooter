using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EffectsManager : SetAsSingleton<EffectsManager> {

    public Color[] destructionColors;
    public float waitBetweenColors;
    public ParticleSystem AttackDroneExplosion;
    public ParticleSystem BombDroneExplosion;

    private GameObject _shotObject;
    private RaycastManager _raycastManager;
    private PoolingManager _poolingManager;

    
    void Awake ()
    {
        _raycastManager = RaycastManager.Instance;
	}


    public void TerminateEnemy()
    {
        _shotObject = _raycastManager.GetCurrentFoundObject();

        if (_shotObject)
        {
            StopEnemyActions(); //TODO
            StartCoroutine(FadeEffect());
        }        
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
    }


    private void StopEnemyActions()
    {
        // TODO AttackDroneActions is null
        //_shotObject.GetComponent<AttackDroneMoveGlow>().IsShot = true;
    }
}
