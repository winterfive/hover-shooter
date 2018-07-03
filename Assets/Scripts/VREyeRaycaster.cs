using System;
using UnityEngine;

namespace VRStandardAssets.Utils
{
    // In order to interact with objects in the scene
    // this class casts a ray into the scene and if it finds
    // a VRInteractiveItem it exposes it for other classes to use.
    // This script should be generally be placed on the camera.
    public class VREyeRaycaster : MonoBehaviour
    {
        public event Action<RaycastHit> OnRaycasthit;                   // This event is called every frame that the user's gaze is over a collider.


        [SerializeField] private Transform _camTransform;
        [SerializeField] private LayerMask _exclusionLayers;           // Layers to exclude from the raycast.
        [SerializeField] private Reticle _reticle;                     // The reticle, if applicable.
        [SerializeField] private VRInput _vrInput;                     // Used to call input based events on the current VRInteractiveItem.
        [SerializeField] private bool _showDebugRay;                   // Optionally show the debug ray.
        [SerializeField] private float _debugRayLength = 5f;           // Debug ray length.
        [SerializeField] private float _debugRayDuration = 1f;         // How long the Debug ray will remain visible.
        [SerializeField] private float _rayLength = 500f;              // How far into the scene the ray is cast.

        
        private VRInteractiveItem _currentInteractible;                //The current interactive item
        private VRInteractiveItem _previousInteractible;               //The last interactive item


        // Utility for other classes to get the current interactive item
        public VRInteractiveItem CurrentInteractible
        {
            get { return _currentInteractible; }
        }

        
        private void OnEnable()
        {
            _vrInput.OnClick += HandleClick;
        }


        private void OnDisable ()
        {
            _vrInput.OnClick -= HandleClick;
        }


        private void Update()
        {
            EyeRaycast();
        }

      
        private void EyeRaycast()
        {
            // Show the debug ray if required
            if (_showDebugRay)
            {
                Debug.DrawRay(_camTransform.position, _camTransform.forward * _debugRayLength, Color.magenta, _debugRayDuration);
            }

            // Create a ray that points forwards from the camera.
            Ray ray = new Ray(_camTransform.position, _camTransform.forward);
            RaycastHit hit;
            
            // Do the raycast forweards to see if we hit an interactive item
            if (Physics.Raycast(ray, out hit, _rayLength, ~_exclusionLayers))
            {
                VRInteractiveItem interactible = hit.collider.GetComponent<VRInteractiveItem>(); //attempt to get the VRInteractiveItem on the hit object
                _currentInteractible = interactible;

                // If we hit an interactive item and it's not the same as the last interactive item, then call Over
                if (interactible && interactible != _previousInteractible)
                    interactible.Over(); 

                // Deactive the last interactive item 
                if (interactible != _previousInteractible)
                    DeactiveLastInteractible();

                _previousInteractible = interactible;

                // Something was hit, set at the hit position.
                if (_reticle)
                    _reticle.SetPosition(hit);

                if (OnRaycasthit != null)
                    OnRaycasthit(hit);
            }
            else
            {
                // Nothing was hit, deactive the last interactive item.
                DeactiveLastInteractible();
                _currentInteractible = null;

                // Position the reticle at default distance.
                if (_reticle)
                    _reticle.SetPosition();
            }
        }


        private void DeactiveLastInteractible()
        {
            if (_previousInteractible == null)
                return;

            _previousInteractible.Out();
            _previousInteractible = null;
        }

        private void HandleClick()
        {
            if (_currentInteractible != null)
                _currentInteractible.Click();
        }
    }
}