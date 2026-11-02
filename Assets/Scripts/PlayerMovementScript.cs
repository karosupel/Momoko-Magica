using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    PlayerAttackScript playerAttack;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 facingDirection = Vector2.down;
    public Vector2 FacingDirection { get {return facingDirection;}}
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    [SerializeField] private Transform attackPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttackScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAttack.IsAttacking)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }

        if(moveInput.x > 0)
        {
            attackPoint.transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y);
        }

        if(moveInput.x < 0)
        {
            attackPoint.transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);
        }

        if(moveInput.y > 0 && moveInput.x == 0)
        {
            attackPoint.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
        }

        if(moveInput.y < 0 && moveInput.x == 0)
        {
            attackPoint.transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);

        if(context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }

        moveInput = context.ReadValue<Vector2>().normalized;
        if (moveInput != Vector2.zero)
        {
            facingDirection = moveInput;
        }
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }

}
