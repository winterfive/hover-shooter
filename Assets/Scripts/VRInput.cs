using System;
using UnityEngine;

namespace VRStandardAssets.Utils
{
    // This class encapsulates all the input required for most VR games.
    // It has events that can be subscribed to by classes that need specific input.
    // This class must exist in every scene and so can be attached to the main
    // camera for ease.
    public class VRInput : MonoBehaviour
    {
        public event Action OnClick;

        private float _previousFireTime;


        private void Update()
        {
            CheckInput();
        }

        // Checks for Fire button input
        // void -> void
        private void CheckInput()
        {
            if (Input.GetButtonDown("Fire1"))
            {            
                // If anything has subscribed to OnDown call it.
                if (OnClick != null)
                    OnClick();

                _previousFireTime = Time.deltaTime;
            }
        }
        

        private void OnDestroy()
        {
            // Ensure that all events are unsubscribed when this is destroyed.
            OnClick = null;
        }
    }
}