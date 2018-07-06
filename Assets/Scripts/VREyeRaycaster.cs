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
        public event Action<RaycastHit> OnRaycasthit;                   

        [SerializeField] private Transform _camTransform;
        [SerializeField] private LayerMask _findWithRaycast;           
        [SerializeField] private Reticle _reticle;                     
        [SerializeField] private VRInput _vrInput;                     
        [SerializeField] private bool _showDebugRay;                   
        [SerializeField] private float _debugRayLength = 5f;           
        [SerializeField] private float _debugRayDuration = 1f;         
        [SerializeField] private float _rayLength = 500f;  
        
        private VRInteractiveItem _currentInteractible;
        private VRInteractiveItem _previousInteractible;

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


        // Shoots raycast into scene, if found, stores interactiveItem, and 
        // manages reticle location
        // void -> void
        private void EyeRaycast()
        {
            // Show the debug ray if required
            if (_showDebugRay)
            {
                Debug.DrawRay(_camTransform.position, _camTransform.forward * _debugRayLength, Color.magenta, _debugRayDuration);
            }

            Ray ray = new Ray(_camTransform.position, _camTransform.forward);
            RaycastHit hit;
            
            // Send raycast forwards to see if we hit an interactive item
            if (Physics.Raycast(ray, out hit, _rayLength, _findWithRaycast))
            {
                VRInteractiveItem interactible = hit.collider.GetComponent<VRInteractiveItem>();
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