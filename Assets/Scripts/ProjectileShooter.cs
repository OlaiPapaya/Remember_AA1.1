using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float shootForce = 10;
    [Tooltip("Set to 0 for infinite lifetime")]
    [SerializeField] private float projectileLifetime = 5;
    [SerializeField] private GameObject shootParticles;
    [SerializeField] private Animator shootAnimator;

    private InputSystem_Actions inputActions;

    void Awake()
    {
        inputActions = new();
    }

    private void OnEnable()
    {
        inputActions.Player.Jump.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Jump.Disable();
    }

    void Update()
    {
        if (inputActions.Player.Jump.WasPressedThisFrame()) Shoot();
    }

    private void Shoot()
    {
        if (projectile == null)
        {
            Debug.LogWarning("NULL REFERENCE EXCEPTION!!!!!! 'Projectile' not set to an instance of an object or something like that.");
            return;
        }

        GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce, ForceMode.Impulse);
        if (projectileLifetime > 0) Destroy(newProjectile, projectileLifetime);

        if (shootParticles != null)
        {
            GameObject particles = Instantiate(shootParticles, transform.position, transform.rotation);
            Destroy(particles, 3);
        }

        if (shootAnimator != null)
        {
            shootAnimator.SetTrigger("Shoot");
        }
    }
}
