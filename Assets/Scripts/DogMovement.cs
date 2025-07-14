using System;
using UnityEngine;

public class DogMovement : MonoBehaviour
{
    public Transform targetPosition;
    public float speed = 1f;

    private bool shouldMove = false;

    void Update()
    {

        if (shouldMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldMove = true;
        }
    }
}