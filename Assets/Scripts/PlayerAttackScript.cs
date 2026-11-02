using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackScript : MonoBehaviour
{
    PlayerMovementScript playerMovement;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    private Animator animator;
    [SerializeField] private int comboCount = 1;
    [SerializeField] private float comboCooldown = 0.8f;
    [SerializeField] LayerMask enemyLayer;
    private float lastComboTime = 0f;
    private float lastAttackTime = 0f;
    [SerializeField] private float attackCooldown = 0.5f;
    private Vector2 facingDirection;
    private bool isAttacking = false;
    public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GetComponent<PlayerMovementScript>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

        AttackEnemies();

        facingDirection = playerMovement.FacingDirection;

        animator.SetFloat("FacingDirectionX", facingDirection.x);
        animator.SetFloat("FacingDirectionY", facingDirection.y);

        animator.SetInteger("ComboCount", comboCount);
        animator.SetTrigger("Attack");
    }

    private void AttackEnemies()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(new Vector2(attackPoint.transform.position.x, attackPoint.transform.position.y), attackRange, enemyLayer);
        foreach (Collider2D enemy in enemies)
        {
            Debug.Log("Hit");
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(new Vector2(attackPoint.transform.position.x, attackPoint.transform.position.y), attackRange);
    }
}
