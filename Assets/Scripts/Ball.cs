using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void ReserPosition()
    {
        transform.position = new Vector3(0, 10, 0);
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            //TODO add points
            ReserPosition();
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            //TODO remove points
            ReserPosition();
        }
    }
}
