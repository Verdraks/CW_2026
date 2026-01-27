using UnityEngine;

public class S_Door : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [Header("References")]
    [SerializeField] private Transform door;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;

    [Header("Input")]
    [SerializeField] private RSE_BasicEvent rse_OpenDoor;
    [SerializeField] private RSE_BasicEvent rse_CloseDoor;
    //[Header("Output")]
    private bool isOpen = false;
    private float t;

    private void OnEnable()
    {
        rse_OpenDoor.Action += OpenDoor;
        rse_CloseDoor.Action += CloseDoor;
    }
    private void OnDisable()
    {
        rse_OpenDoor.Action -= OpenDoor;
        rse_CloseDoor.Action -= CloseDoor;
    }
    private void Awake()
    {
        door.position = startPos.position;
        door.rotation = startPos.rotation;
        t = 0f;
    }

    private void Update()
    {
        float target = isOpen ? 1f : 0f;

        if(!Mathf.Approximately(t, target))
        {
            t = Mathf.MoveTowards(t, target, Time.deltaTime * speed);
            float easedT = curve.Evaluate(t);
            door.position = Vector3.Lerp(startPos.position, endPos.position, easedT);
            door.rotation = Quaternion.Slerp(startPos.rotation, endPos.rotation, easedT);
        }
    }
    private void OpenDoor()
    {
        isOpen = true;
    }
    private void CloseDoor()
    {
        isOpen = false;
    }
}