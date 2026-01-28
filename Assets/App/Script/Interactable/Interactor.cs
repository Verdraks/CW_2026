using UnityEngine;

public class Interactor : MonoBehaviour
{
        [Header("Settings")]
        [SerializeField] private LayerMask m_InteractableLayerMask;
        [SerializeField] private float m_InteractDistance = 3f;
        
        [Header("Inputs")]
        [SerializeField] private RSE_OnPlayerInteract m_OnPlayerInteract;
        
        private IInteractable m_CurrentInteractable;
        private bool m_IsInteracting;
        // The interactable currently being hovered by the raycast (looked at)
        private IInteractable m_HoveredInteractable;
        
        private void OnEnable()
        {
                m_OnPlayerInteract.Action += OnInteract;
        }
        
        private void OnDisable()
        {
                m_OnPlayerInteract.Action -= OnInteract;
        }

        private void Update()
        {
                // Raycast each frame to detect the interactable we're looking at
                bool didHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, m_InteractDistance, m_InteractableLayerMask);
                if (didHit && hit.collider.TryGetComponent(out IInteractable interactable))
                {
                        // Entered a new hover target
                        if (interactable != m_HoveredInteractable)
                        {
                                if (m_HoveredInteractable != null)
                                {
                                        m_HoveredInteractable.Unhover();
                                }
                                m_HoveredInteractable = interactable;
                                m_HoveredInteractable.Hover();
                        }
                }
                else
                {
                        // No interactable hit: clear previous hover
                        if (m_HoveredInteractable != null)
                        {
                                m_HoveredInteractable.Unhover();
                                m_HoveredInteractable = null;
                        }
                }
                
                // If we're interacting, ensure we still look at the same interactable; otherwise stop interaction
                if (m_IsInteracting && m_CurrentInteractable != null)
                {
                        if (!didHit || !hit.collider.TryGetComponent(out IInteractable hitInteractable) || hitInteractable != m_CurrentInteractable)
                        {
                                StopInteraction();
                        }
                }
        }

        private void OnInteract(bool isPressed)
        {
                if (isPressed)
                {
                        TryStartInteraction();
                }
                else
                {
                        StopInteraction();
                }
        }

        private void TryStartInteraction()
        {
                Debug.DrawRay(transform.position, transform.forward * m_InteractDistance, Color.red, 1f);
                if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, m_InteractDistance,
                            m_InteractableLayerMask)) return;
                if (hit.collider.TryGetComponent(out IInteractable interactable))
                {
                        m_CurrentInteractable = interactable;
                        m_IsInteracting = true;
                        interactable.Interact();
                }
        }

        private void StopInteraction()
        {
                if (m_CurrentInteractable != null)
                {
                        m_CurrentInteractable.StopInteract();
                        m_CurrentInteractable = null;
                }
                m_IsInteracting = false;
        }
}