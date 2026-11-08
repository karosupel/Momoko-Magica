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
        animator.SetBool("isWalking", true);

        animator.SetFloat("InputX", direction.x);
        animator.SetFloat("InputY", direction.y);

        animator.SetFloat("LastInputX", direction.x);
        animator.SetFloat("LastInputY", direction.y);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if(attackAfter)
        {
            if(Vector2.Distance(player.transform.position, targetPosition) >= player.AttackRange)
            {
                direction = (targetPosition - player.transform.position).normalized;
                rb.linearVelocity = direction * moveSpeed;
            }
            else
            {
                animator.SetBool("isWalking", false);
                rb.linearVelocity = Vector2.zero;
                player.Direction = direction;
                player.SwitchState(player.attackState);
            }
        }
        else if(!attackAfter)
        {
            if(Vector2.Distance(player.transform.position, targetPosition) > 0.1f) //podmienić na dokładność, to też do dorzucenia do parametrów
            {
                direction = (targetPosition - player.transform.position).normalized;
                rb.linearVelocity = direction * moveSpeed;
            }
            else
            {
                animator.SetBool("isWalking", false);
                player.Direction = direction;
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    //TO DO: dorzucić omijanie jesli moving state jest nieskonczony, czyli jakaś kontrolka czasu
}
