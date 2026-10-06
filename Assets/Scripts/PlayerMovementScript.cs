using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 facingDirection = Vector2.down;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;

    [SerializeField] private int comboCount = 1;
    [SerializeField] private float comboCooldown = 0.8f;
    [SerializeField] private float lastComboTime = 0f;
    [SerializeField] private float lastAttackTime = 0f;
    [SerializeField] private float attackCooldown = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
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

    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        if (Time.time - lastAttackTime < attackCooldown)
        {
            return;
        }

        if (Time.time - lastComboTime > comboCooldown)
        {
            comboCount = 1;
        }
        else
        {
            if (comboCount < 3)
            {
                comboCount++;
            }
            else
            {
                comboCount = 1;
            }
        }
        lastAttackTime = Time.time;
        lastComboTime = Time.time;

        animator.SetFloat("FacingDirectionX", facingDirection.x);
        animator.SetFloat("FacingDirectionY", facingDirection.y);

        animator.SetInteger("ComboCount", comboCount);
        animator.SetTrigger("Attack");
    }

}
