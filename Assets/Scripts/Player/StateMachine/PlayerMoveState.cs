using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    private Vector3 direction;
    private Vector3 targetPosition;
    public Vector3 TargetPosition
    {
        get { return targetPosition; }
        set
        {
            targetPosition = value;
            isInputMovement = false;
            isMovingToTarget = true;
        }
    }
    private float moveSpeed;
    private bool attackAfter;
    private Vector2 inputDirection;
    private bool isInputMovement;
    private bool isMovingToTarget;
    public bool IsInputMovement { get { return isInputMovement; } }
    public bool IsMoving { get { return isInputMovement || isMovingToTarget; } }
    public bool AttackAfter { get { return attackAfter; } set { attackAfter = value; } }
    Rigidbody2D rb;
    Animator animator;

    public void SetInputDirection(Vector2 value)
    {
        inputDirection = value;
        isInputMovement = true;
        isMovingToTarget = false;
    }

    public void StopInputMovement()
    {
        inputDirection = Vector2.zero;
        isInputMovement = false;
        isMovingToTarget = false;
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("isWalking", false);
    }

    public override void EnterState(PlayerStateManager player)
    {
        rb = player.GetComponent<Rigidbody2D>();
        animator = player.GetComponent<Animator>();
        moveSpeed = player.MoveSpeed;
        direction = isInputMovement
            ? (Vector3)inputDirection.normalized
            : (targetPosition - player.transform.position).normalized;
        UpdateFacingDirection(player);
        animator.SetBool("isWalking", true);

    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (isInputMovement)
        {
            direction = inputDirection.normalized;
            UpdateFacingDirection(player);
            animator.SetBool("isWalking", true);
            rb.linearVelocity = direction * moveSpeed;
            return;
        }

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
                isMovingToTarget = false;
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
                isMovingToTarget = false;
                animator.SetBool("isWalking", false);
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    private void UpdateFacingDirection(PlayerStateManager player)
    {
        if (direction.sqrMagnitude == 0f)
            return;

        Vector2 facingDirection = player.GetCardinalFacingDirection(direction);

        player.Direction = facingDirection;
        player.AdjustAttackPointDirection(facingDirection);
        animator.SetFloat("InputX", facingDirection.x);
        animator.SetFloat("InputY", facingDirection.y);
        animator.SetFloat("LastInputX", facingDirection.x);
        animator.SetFloat("LastInputY", facingDirection.y);
    }

    //TO DO: dorzucić omijanie jesli moving state jest nieskonczony, czyli jakaś kontrolka czasu
}
