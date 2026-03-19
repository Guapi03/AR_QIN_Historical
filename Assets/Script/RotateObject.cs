using UnityEngine;
using Vuforia;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 3f;

    [Header("Start Rotation Reference")]
    public Transform startTransform;

    float currentY;
    bool isDragging = false;

    float baseX;
    float baseY;
    float baseZ;

    ObserverBehaviour observer;

    void Start()
    {
        if (startTransform != null)
        {
            Vector3 baseRot = startTransform.localEulerAngles;

            baseX = baseRot.x;
            baseY = baseRot.y;
            baseZ = baseRot.z;
        }
        else
        {
            Vector3 baseRot = transform.localEulerAngles;

            baseX = baseRot.x;
            baseY = baseRot.y;
            baseZ = baseRot.z;
        }

        ResetRotation();

        observer = GetComponentInParent<ObserverBehaviour>();

        if (observer != null)
        {
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            ResetRotation();
        }
    }

    void ResetRotation()
    {
        currentY = 0f;

        transform.localRotation = Quaternion.Euler(
            baseX,
            baseY,
            baseZ
        );
    }

    void Update()
    {
        // 手机触摸
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                isDragging = true;

            if (touch.phase == TouchPhase.Moved && isDragging)
            {
                float rot = -touch.deltaPosition.x * rotationSpeed;
                currentY += rot;
            }

            if (touch.phase == TouchPhase.Ended)
                isDragging = false;
        }

#if UNITY_EDITOR
        // 电脑鼠标
        if (Input.GetMouseButtonDown(0))
            isDragging = true;

        if (Input.GetMouseButton(0) && isDragging)
        {
            float rot = Input.GetAxis("Mouse X") * rotationSpeed * 100;
            currentY += rot;
        }

        if (Input.GetMouseButtonUp(0))
            isDragging = false;
#endif

        transform.localRotation = Quaternion.Euler(
            baseX,
            baseY + currentY,
            baseZ
        );
    }
}