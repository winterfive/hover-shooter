﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : GenericManager<EffectsManager> {

    public Color[] destructionColors;
    public float waitBetweenColors;

    private GameObject _shotObject;
    private RaycastManager _raycastManager;

    
    void Awake ()
    {
        _raycastManager = RaycastManager.Instance;
	}


    public void EnemyDisapears()
    {
        _shotObject = _raycastManager.GetCurrentFoundObject();
    }


    private IEnumerator FadeEffect()
    {
        // TODO This required turning on transparent in the material which is not good for mobile :(
        // Change fade out to changing to darker red color then particle explosion using that color
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
                Debug.Log("Fading out drone to color: " + c);
            }

            yield return new WaitForSeconds(waitBetweenColors);
        }
    }
}