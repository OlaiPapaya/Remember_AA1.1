using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private GameObject targetMesh;
    [SerializeField] private GameObject brokenTargetMesh;
    [SerializeField] private float respawnTime;
    [SerializeField] private int destroyPoints = 100;

    private MovingObject movementScript;

    private bool destroyed;
    private float destroyedTime;

    private void Start()
    {
        movementScript = GetComponent<MovingObject>();
    }

    private void Update()
    {
        // If the target is destroyed, when "respawnTime" seconds
        // have passed, it will be respawned again.
        if (destroyed && Time.time >= destroyedTime + respawnTime)
        {
            TargetRespawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Projectile") || destroyed) return;
        TargetDestroyed();
    }

    void TargetDestroyed()
    {
        // Create a broken target with the same transform as this target,
        // and destroy it when the real target respawns again.
        GameObject brokenTarget = Instantiate(brokenTargetMesh, transform.position, transform.rotation);
        brokenTarget.transform.localScale = transform.localScale;
        brokenTarget.GetComponent<BrokenTargetReconstructor>().TargetDestroyed(respawnTime);

        targetMesh.SetActive(false);
        destroyed = true;
        destroyedTime = Time.time;

        ScoreSystem.instance?.AddScore(destroyPoints);

        if (movementScript != null) movementScript.Pause();
    }

    void TargetRespawn()
    {
        targetMesh.SetActive(true);
        destroyed = false;
        if (movementScript != null) movementScript.Resume();
    }
}
