// using UnityEngine;
//
// public class S_DraggableObjectTracking : S_ObjectTrackingBase
// {
//     private bool isDragging = false;
//     private Vector3 lastPosition;
//     private float totalDragDistance = 0f;
//
//     protected override void InitializeTracking()
//     {
//         isDragging = false;
//         lastPosition = transform.position;
//         totalDragDistance = 0f;
//         
//         if (m_TrackingData != null && m_TrackingData.DragDistance != null)
//         {
//             m_TrackingData.DragDistance.Set(0f);
//         }
//         if (m_TrackingData != null && m_TrackingData.IsManipulated != null)
//         {
//             m_TrackingData.IsManipulated.Set(false);
//         }
//     }
//
//     protected override void UpdateTracking()
//     {
//         if (m_TrackingData != null && m_TrackingData.DragDistance != null)
//         {
//             m_TrackingData.DragDistance.Set(totalDragDistance);
//         }
//     }
//
//     protected override void ResetTracking()
//     {
//         isDragging = false;
//         lastPosition = transform.position;
//         totalDragDistance = 0f;
//         
//         if (m_TrackingData != null && m_TrackingData.DragDistance != null)
//         {
//             m_TrackingData.DragDistance.Set(0f);
//         }
//         if (m_TrackingData != null && m_TrackingData.IsManipulated != null)
//         {
//             m_TrackingData.IsManipulated.Set(false);
//         }
//     }
//
//     private void Update()
//     {
//         if (isDragging)
//         {
//             float distance = Vector3.Distance(transform.position, lastPosition);
//             totalDragDistance += distance;
//             lastPosition = transform.position;
//             UpdateTracking();
//         }
//     }
//
//     /// <summary>
//     /// Appelé lorsque l'utilisateur commence à faire glisser l'objet.
//     /// Cette méthode doit être appelée par un système d'input externe.
//     /// </summary>
//     public void OnDragStarted()
//     {
//         isDragging = true;
//         lastPosition = transform.position;
//         
//         if (m_TrackingData != null && m_TrackingData.IsManipulated != null)
//         {
//             m_TrackingData.IsManipulated.Set(true);
//         }
//     }
//
//     /// <summary>
//     /// Appelé lorsque l'utilisateur arrête de faire glisser l'objet.
//     /// </summary>
//     public void OnDragEnded()
//     {
//         isDragging = false;
//         
//         if (m_TrackingData != null && m_TrackingData.IsManipulated != null)
//         {
//             m_TrackingData.IsManipulated.Set(false);
//         }
//     }
// }
//
