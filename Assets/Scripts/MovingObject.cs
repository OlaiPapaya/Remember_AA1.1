using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private List<Transform> destinations = new();
    [SerializeField] private float allowedDestinationDistance = 0.2f;
    [SerializeField] private float speed = 10;
    [SerializeField] private float acceleration = 1;

    private int currentDestinationIdx = 0;
    private Vector3 currentMovement;

    private void Start()
    {
        if (destinations.Count <= 0)
        {
            Debug.LogWarning("Destinations list is empty! Script annihilated.");
            Destroy(this);
        }
    }

    void Update()
    {
        CheckDestinationArrival();
        Movement();
    }

    void CheckDestinationArrival()
    {
        if (Vector3.Distance(transform.position, destinations[currentDestinationIdx].position) > allowedDestinationDistance)
            return;

        currentDestinationIdx++;
        if (currentDestinationIdx >= destinations.Count) currentDestinationIdx = 0;
    }

    void Movement()
    {
        Vector3 dirToDestination = (destinations[currentDestinationIdx].position - transform.position).normalized;
        currentMovement += dirToDestination * acceleration * Time.deltaTime;
        currentMovement = Vector3.ClampMagnitude(currentMovement, 1);
        transform.position += currentMovement * speed * Time.deltaTime;
    }
}
