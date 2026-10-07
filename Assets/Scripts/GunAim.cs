using UnityEngine;

public class GunAim : MonoBehaviour
{
    [SerializeField] private float sensitivity = 1;

    [SerializeField] private float horizontalRangeAngle = 60;
    [SerializeField] private float verticalRangeAngle = 80;

    [SerializeField] private Transform baseTransform;
    [SerializeField] private Transform torsoTransform;

    private InputSystem_Actions inputActions;

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

    void Update()
    {
        Vector2 mouseDelta = inputActions.Player.Look.ReadValue<Vector2>() * sensitivity;

        // Saving the current turret's rotation
        Vector2 currentRotation = new(
            torsoTransform.eulerAngles.x,
            baseTransform.eulerAngles.y
        );

        // Calculating the next turret's rotation using player input
        Vector2 updatedRotation = new(
            currentRotation.x - mouseDelta.y,
            currentRotation.y + mouseDelta.x
        );
        
        // Apply the processed rotation
        baseTransform.eulerAngles = new(
            baseTransform.eulerAngles.x,
            updatedRotation.y,
            baseTransform.eulerAngles.z
            );
        torsoTransform.eulerAngles = new(
            updatedRotation.x,
            torsoTransform.eulerAngles.y,
            torsoTransform.eulerAngles.z
            );
    }
}
