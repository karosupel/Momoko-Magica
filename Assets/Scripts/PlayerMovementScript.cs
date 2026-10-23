using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 facingDirection = Vector2.down;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private bool isAttacking = false;

    [SerializeField] private int comboCount = 1;
    [SerializeField] private float comboCooldown = 0.8f;
    private float lastComboTime = 0f;
    private float lastAttackTime = 0f;
    [SerializeField] private float attackCooldown = 0.5f;

    [SerializeField] List<GameObject> attackableEnemies;
    [SerializeField] private Transform attackPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
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

        isAttacking = true;

        animator.SetFloat("FacingDirectionX", facingDirection.x);
        animator.SetFloat("FacingDirectionY", facingDirection.y);

        animator.SetInteger("ComboCount", comboCount);
        animator.SetTrigger("Attack");
    }

    public void EndAttack()
    {
        isAttacking = false;
    }

    public void EnableAttackHitbox()
    {
        HitEnemiesInRange(attackableEnemies);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            attackableEnemies.Add(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            attackableEnemies.Remove(collision.gameObject);
        }
    }

    private void HitEnemiesInRange(List<GameObject> enemies)
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<HealthScript>().GetHit(10);
            Debug.Log("wykonuje sie hit enemies in range");
        }
    }
}
