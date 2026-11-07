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
    private bool verticalPriorityActive;

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

        if(moveInput.y > 0)
        {
            attackPoint.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
        }

        if(moveInput.y < 0)
        {
            attackPoint.transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
        }

        if(moveInput.x > 0 && moveInput.y == 0)
        {
            attackPoint.transform.position = new Vector2(transform.position.x + 0.5f , transform.position.y);
        }

        if(moveInput.x < 0 && moveInput.y == 0)
        {
            attackPoint.transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().normalized;
        if (moveInput != Vector2.zero)
        {
            if (moveInput.y != 0)
            {
                facingDirection = new Vector2(0f, Mathf.Sign(moveInput.y));
                verticalPriorityActive = true;
            }
            else if (!verticalPriorityActive)
            {
                facingDirection = new Vector2(Mathf.Sign(moveInput.x), 0f);
            }
        }
        else
        {
            verticalPriorityActive = false;
            //jeśli puszcze wszystkie klawisze to trace prioryty i mogę atakować lewo prawo
        }

        animator.SetBool("isWalking", moveInput != Vector2.zero);

        if(context.canceled)
        {
            animator.SetFloat("LastInputX", facingDirection.x);
            animator.SetFloat("LastInputY", facingDirection.y);
        }

        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }

}
