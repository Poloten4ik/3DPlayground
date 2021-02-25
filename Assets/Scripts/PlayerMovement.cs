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
    [SerializeField] private float rotationSpeed = 1f;

    [Header("Gravity")]
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float gravityScale = 2;


    [Header("references")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator anim;

    private float gravity;

    private Vector3 startPosition;
    private bool isResetting;

    private Camera mainCamera;
    public void DoDamage()
    {
        //TODO check lives;
        anim.SetTrigger("Death");
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
        transform.DOMove(startPosition, 1f).SetDelay(1f).OnComplete(FinishReset);
        //StartCoroutine(ResetPositionCoroutine());
    }

    private void FinishReset()
    {
        isResetting = false;
    }
    private void Start()
    {
        startPosition = transform.position;
        mainCamera = Camera.main;

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
     

        Move();
    }

    private void Move()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0;
        forward.Normalize()
;        Vector3 right = mainCamera.transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 moveDirection = forward * inputV + right * inputH;

        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }

        if(Mathf.Abs(inputH) > 0 || Mathf.Abs(inputV) > 0)
        {
            anim.SetBool("Running", true);
            //transform.rotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection),rotationSpeed);
           
        }
        else
        {
            anim.SetBool("Running", false);
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

        //bool isJumping = gravity > 0 || gravity < -0.2f && !controller.isGrounded;
        //anim.SetBool("Jump", isJumping);
        if (gravity > 0)
        {
            anim.SetInteger("Gravity", 1);
        }
        else if (gravity < -0.1f)
        {
            anim.SetInteger("Gravity", -1);
        }
        else
        {
            anim.SetInteger("Gravity", 0);
        }

        moveDirection.y = gravity;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
