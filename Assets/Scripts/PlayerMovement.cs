using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;

    [SerializeField]
    private float rotationSpeed = 11;

    [SerializeField]
    private float jumpForce = 10;

    [Header("Pushing Objects")]
    [SerializeField]
    private GameObject objectCenter;

    [SerializeField]
    private float objectRadious = 1f;

    [SerializeField]
    private LayerMask objectsMask;

    [SerializeField]
    private float maxPushForce = 1f;

    [SerializeField]
    private float pushHeight = 5f;

    Rigidbody rb;

    float pushForce;
    bool isGoingUp;

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce);
        }

        if (Input.GetMouseButtonDown(0))
        {
            pushForce = 0;
            isGoingUp = true;
        }

        if (Input.GetMouseButton(0))
        {
            if (isGoingUp)
            {
                pushForce += maxPushForce * Time.deltaTime;
                if (pushForce >= maxPushForce)
                {
                    isGoingUp = false;
                }
            }
            else
            {
                pushForce -= maxPushForce * Time.deltaTime;
                if (pushForce <= 0)
                {
                    isGoingUp = true;
                }
            }
            print(pushForce);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Collider[] colliders = Physics.OverlapSphere(objectCenter.transform.position, objectRadious, objectsMask);
            foreach(Collider col in colliders)
            {
                Rigidbody colRb = col.GetComponent<Rigidbody>();

                Vector3 forceDirection = transform.forward * pushForce;
                forceDirection.y = pushHeight;
                colRb.AddForce(forceDirection.normalized * pushForce, ForceMode.Impulse);
            }
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public float GetForcePercentage()
    {
        return pushForce / maxPushForce;
    }

    private void FixedUpdate()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        //rb.velocity = transform.forward * inputVertical * speed;

        rb.AddForce(transform.forward * inputVertical * speed);

        transform.RotateAround(transform.position, Vector3.up, inputHorizontal * rotationSpeed);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(objectCenter.transform.position, objectRadious);

    }

}
