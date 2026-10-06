using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;

    [SerializeField] private int comboCount = 1;
    [SerializeField] private float comboCooldown = 0.5f;

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
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }

    public void Attack(InputAction.CallbackContext context)
    {
        animator.SetBool("isAttacking", true);
        if(context.performed)
        {
            if(comboCount < 3)
            {
                comboCount++;
            }
            else
            {
                comboCount = 1;
            }
            animator.SetInteger("ComboCount", comboCount);
        }
        if(context.canceled)
        {
            animator.SetBool("isAttacking", false);
        }
    }
}
