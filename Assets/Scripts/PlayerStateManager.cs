using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    PlayerBaseState currentState;
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerMoveState moveState = new PlayerMoveState();
    public PlayerAttackState attackState = new PlayerAttackState();
    Animator animator;
    public Vector2 direction = Vector2.down;

        [Header("Movement Parameters")]
    [SerializeField] private float moveSpeed;
    [SerializeField, Range(0f, 45f)] private float horizontalDirectionTolerance = 15f;

        [Header("Attack Parameters")]
    public Transform attackPoint;
    public LayerMask enemyLayer;
    [SerializeField] private float attackRange;
    [SerializeField] private float damageLight;
    [SerializeField] private float damageHeavy;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float lastAttackTime = 0f;

        [Header("Combo Parameters")]
    [SerializeField] private int comboCount = 1;
    [SerializeField] private float comboCooldown = 0.8f;
    [SerializeField] private float lastComboTime = 0f;
    

    //getters & setters
    public float MoveSpeed { get { return moveSpeed; } }
    public float HorizontalDirectionTolerance { get { return horizontalDirectionTolerance; } }
    public float AttackRange { get { return attackRange; } set { attackRange = value; } }
    public float DamageLight { get { return damageLight; } set { damageLight = value; } }
    public float DamageHeavy { get { return damageHeavy; } set { damageHeavy = value; } }
    public float AttackCooldown { get { return attackCooldown; } set { attackCooldown = value; } }
    public float LastAttackTime { get { return lastAttackTime; } set { lastAttackTime = value; } }
    public int ComboCount { get { return comboCount; } set { comboCount = value; } }
    public float ComboCooldown { get { return comboCooldown; } set { comboCooldown = value; } }
    public float LastComboTime { get { return lastComboTime; } set { lastComboTime = value; } }
    public Vector2 Direction { get { return direction; } set { direction = value; } }

    void Start()
    {
        animator = GetComponent<Animator>();
        currentState = idleState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(
            Mouse.current.position.ReadValue()
        );

        Collider2D hit = Physics2D.OverlapPoint(mousePosition);

        if (hit != null)
        {
            int layer = hit.gameObject.layer;
            direction = (hit.gameObject.transform.position - transform.position).normalized;
            AdjustAttackPointDirection(direction);

            if (layer == LayerMask.NameToLayer("Ground"))
            {
                Debug.Log("ground");
                currentState = moveState;
                moveState.TargetPosition = mousePosition;
                moveState.AttackAfter = false;
                AdjustAttackPointDirection(direction);
                moveState.EnterState(this);
            }
            else if (layer == LayerMask.NameToLayer("Enemy"))
            {
                Debug.Log("enemy");
                if(Vector2.Distance(attackPoint.transform.position, hit.gameObject.transform.position) <= attackRange) //to podmienic potem na bycie w zasiegu
                {
                    AdjustAttackPointDirection(direction);
                    SwitchState(attackState);
                }
                else
                {
                    currentState = moveState;
                    moveState.TargetPosition = mousePosition;
                    moveState.AttackAfter = true;
                    AdjustAttackPointDirection(direction);
                    moveState.EnterState(this);
                }
            }
        }
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void AdjustAttackPointDirection(Vector2 newDirection)
    {
        if(newDirection.y > 0)
        {
            attackPoint.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
        }

        if(newDirection.y < 0)
        {
            attackPoint.transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
        }

        if(newDirection.x > 0 && Mathf.Round(newDirection.y) == 0)
        {
            attackPoint.transform.position = new Vector2(transform.position.x + 0.5f , transform.position.y);
        }

        if(newDirection.x < 0 && Mathf.Round(newDirection.y) == 0)
        {
            attackPoint.transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);
        }
    }
}
