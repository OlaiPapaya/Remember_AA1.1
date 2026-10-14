using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private int touchPoints = -100;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Projectile")) return;
        ScoreSystem.instance?.AddScore(touchPoints);
        Destroy(other.gameObject);
    }
}
