using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement Instance { get; private set; }
    

    [Header("Movement config")]
    [SerializeField] private float moveSpeed = 10f;

    [Header("Rotation config")]
    [SerializeField] private float rotationSpeed = 100f;

    [Header("Gravity")]
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float gravityScale = 2;


    [Header("references")]
    [SerializeField] private CharacterController controller;

    private float gravity;

    private Vector3 startPosition;
    private bool isResetting;

    public void DoDamage()
    {
        //TODO check lives;

        ResetPosition();
    }

    IEnumerator ResetPositionCoroutine()
    {
        isResetting = true;
        transform.position = startPosition;
        yield return new WaitForSeconds(0.1f);
        isResetting = false;
    }

    private void ResetPosition()
    {
        isResetting = true;
        transform.DOMove(startPosition, 1f).OnComplete(FinishReset);
        //StartCoroutine(ResetPositionCoroutine());
    }

    private void FinishReset()
    {
        isResetting = false;
    }
    private void Start()
    {
        startPosition = transform.position;
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        if(isResetting)
        {
            return;
        }

        Rotate();
        Move();
    }

    private void Move()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * inputV + transform.right * inputH;

        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }

       
        if (controller.isGrounded)
        {
            gravity = -0.1f;
            if (Input.GetButtonDown("Jump"))
            {
                gravity = jumpHeight;
            }

        }
        else
        {
            gravity += gravityScale * Physics.gravity.y * Time.deltaTime;
        }

        moveDirection.y = gravity;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        float mouseHorizontal = Input.GetAxis("Mouse X");

        transform.Rotate(Vector3.up, mouseHorizontal * rotationSpeed * Time.deltaTime);
    }
}
