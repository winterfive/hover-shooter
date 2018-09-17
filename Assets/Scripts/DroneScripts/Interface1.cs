using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Drone_Scripts
{
    interface LookAt
    {
        /*
     * Turns drone turret towards player
     * void -> void
     */
        private void LookAtPlayer(Transform t)
        {
            Vector3 newVector = new Vector3(t.transform.position.x - _camTransform.position.x,
                                            0f,
                                            t.transform.position.z - _camTransform.position.z);

            t.transform.rotation = Quaternion.LookRotation(newVector);
        }
    }
}
