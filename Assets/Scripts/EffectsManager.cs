using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour {
    

    void Explode(GameObject go)
    {
        // Listen for player shooting drone
        // Each gameobject w/ animation particle system will have fields for
        // particleTimeLenghth and particleWaitTime
        // Begin explosion particles
        // Allow for different particles systems to be called by this method
        // via drone tag
    }

    void SmokeTrail()
    {
        // Smoke trail rises from shot down drone, short duration
    }
}
