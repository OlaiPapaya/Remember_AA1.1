using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float horizontalSensitivity;
    [SerializeField] private float verticalSensitivity;
    [SerializeField] private float verticalRotationLimit;
    [SerializeField] private float cameraDistance;
    [SerializeField] private float cameraWallSnapMargin = 0.1f;

    private Vector3 currentRotation;

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new();
    }
    private void OnEnable()
    {
        inputActions.Player.Move.Enable();
    }
    private void OnDisable()
    {
        inputActions.Player.Move.Disable();
    }

    void Update()
    {
        ChangeCameraDistance();

        Vector2 deltaRotation = inputActions.Player.Move.ReadValue<Vector2>() * Time.deltaTime;
        deltaRotation.x *= horizontalSensitivity;
        deltaRotation.y *= verticalSensitivity;

        currentRotation = new Vector3(
            Mathf.Clamp(currentRotation.x + deltaRotation.y, -verticalRotationLimit, verticalRotationLimit),
            currentRotation.y - deltaRotation.x,
            currentRotation.z
        );

        transform.localEulerAngles = currentRotation;
    }

    void ChangeCameraDistance()
    {
        float realCameraDistance = cameraDistance;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -cameraTransform.forward, out hit, cameraDistance))
        {
            realCameraDistance = hit.distance - cameraWallSnapMargin;
        }

        cameraTransform.localPosition = new(0, 0, -realCameraDistance);
    }
}
