using UnityEngine;

namespace Assets.Scripts.Drone_Scripts
{
    interface LookAt
    {
        /*
     * Turns drone object towards another object
     * Transform -> void
     */
        void LookAt(Transform t);
    }
}
