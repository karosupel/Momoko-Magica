using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    private Vector3 direction;
    private Vector3 targetPosition;
    public Vector3 TargetPosition { get { return targetPosition; } set { targetPosition = value; } }
    private float moveSpeed;
    private bool attackAfter;
    public bool AttackAfter { get { return attackAfter; } set { attackAfter = value; } }
    Rigidbody2D rb;
    Animator animator;
    public override void EnterState(PlayerStateManager player)
    {
        rb = player.GetComponent<Rigidbody2D>();
        animator = player.GetComponent<Animator>();
        moveSpeed = player.MoveSpeed;
        direction = (targetPosition - player.transform.position).normalized;
        UpdateFacingDirection(player);
        animator.SetBool("isWalking", true);

    }

    public override void UpdateState(PlayerStateManager player)
    {
        if(attackAfter)
        {
            direction = (targetPosition - player.transform.position).normalized;
            UpdateFacingDirection(player);

            if(Vector2.Distance(player.attackPoint.transform.position, targetPosition) >= player.AttackRange)
            {
                rb.linearVelocity = direction * moveSpeed;
            }
            else
            {
                animator.SetBool("isWalking", false);
                rb.linearVelocity = Vector2.zero;
                player.SwitchState(player.attackState);
            }
        }
        else if(!attackAfter)
        {
            if(Vector2.Distance(player.transform.position, targetPosition) > 0.1f) //podmienić na dokładność, to też do dorzucenia do parametrów
            {
                direction = (targetPosition - player.transform.position).normalized;
                UpdateFacingDirection(player);
                rb.linearVelocity = direction * moveSpeed;
            }
            else
            {
                animator.SetBool("isWalking", false);
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    private void UpdateFacingDirection(PlayerStateManager player)
    {
        if (direction.sqrMagnitude == 0f)
            return;

        Vector2 facingDirection;
        float angleFromHorizontal = Mathf.Atan2(Mathf.Abs(direction.y), Mathf.Abs(direction.x)) * Mathf.Rad2Deg;
        if (angleFromHorizontal <= player.HorizontalDirectionTolerance)
            facingDirection = new Vector2(Mathf.Sign(direction.x), 0f);
        else
            facingDirection = new Vector2(0f, Mathf.Sign(direction.y));

        player.Direction = facingDirection;
        player.AdjustAttackPointDirection(facingDirection);
        animator.SetFloat("InputX", facingDirection.x);
        animator.SetFloat("InputY", facingDirection.y);
        animator.SetFloat("LastInputX", facingDirection.x);
        animator.SetFloat("LastInputY", facingDirection.y);
    }

    //TO DO: dorzucić omijanie jesli moving state jest nieskonczony, czyli jakaś kontrolka czasu
}
