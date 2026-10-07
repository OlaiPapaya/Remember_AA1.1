using UnityEngine;

public class GunAim : MonoBehaviour
{
    [SerializeField] private float sensitivity = 1;

    [SerializeField] private float horizontalRangeAngle = 60;
    [SerializeField] private float verticalRangeAngle = 80;

    [SerializeField] private Transform baseTransform;
    [SerializeField] private Transform torsoTransform;

    private InputSystem_Actions inputActions;

    private Vector2 currentRotation;

    void Awake()
    {
        inputActions = new();
    }

    private void OnEnable()
    {
        inputActions.Player.Look.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        inputActions.Player.Look.Disable();
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        currentRotation = new(
            torsoTransform.localEulerAngles.x,
            baseTransform.localEulerAngles.y
        );
    }

    void Update()
    {
        Vector2 mouseDelta = inputActions.Player.Look.ReadValue<Vector2>() * sensitivity;

        // Calculating the next turret's rotation using player input
        currentRotation = new(
            currentRotation.x - mouseDelta.y,
            currentRotation.y + mouseDelta.x
        );

        if (verticalRangeAngle != 0)
            currentRotation.x = Mathf.Clamp(currentRotation.x, -verticalRangeAngle, verticalRangeAngle);
        if (horizontalRangeAngle != 0)
            currentRotation.y = Mathf.Clamp(currentRotation.y, -horizontalRangeAngle, horizontalRangeAngle);
        
        // Apply the processed rotation
        baseTransform.localEulerAngles = new(
            baseTransform.localEulerAngles.x,
            currentRotation.y,
            baseTransform.localEulerAngles.z
            );
        torsoTransform.localEulerAngles = new(
            currentRotation.x,
            torsoTransform.localEulerAngles.y,
            torsoTransform.localEulerAngles.z
            );
    }
}
