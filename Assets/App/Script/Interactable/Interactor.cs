using UnityEngine;

public class Interactor : MonoBehaviour
{
        [Header("Settings")]
        [SerializeField] private LayerMask m_InteractableLayerMask;
        [SerializeField] private float m_InteractDistance = 3f;
        
        [Header("Inputs")]
        [SerializeField] private RSE_OnPlayerInteract m_OnPlayerInteract;
        
        private void OnEnable()
        {
                m_OnPlayerInteract.Action += Interact;
        }
        
        private void OnDisable()
        {
                m_OnPlayerInteract.Action -= Interact;
        }

        private void Interact()
        {
                Debug.DrawRay(transform.position, transform.forward * m_InteractDistance, Color.red, 1f);
                if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, m_InteractDistance,
                            m_InteractableLayerMask)) return;
                if (hit.collider.TryGetComponent(out IInteractable interactable))
                {
                        interactable.Interact();
                }
        }
}