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
                if (!m_IsInteracting || m_CurrentInteractable == null) return;
                
                // Check if player is still looking at the same interactable
                if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, m_InteractDistance, m_InteractableLayerMask) ||
                    !hit.collider.TryGetComponent(out IInteractable interactable) ||
                    interactable != m_CurrentInteractable)
                {
                        StopInteraction();
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