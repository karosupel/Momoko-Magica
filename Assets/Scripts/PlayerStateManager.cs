using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    PlayerBaseState currentState;
    PlayerIdleState idleState = new PlayerIdleState();
    PlayerMoveState moveState = new PlayerMoveState();
    Animator animator;

    [SerializeField] float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }
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

            if (layer == LayerMask.NameToLayer("Ground"))
            {
                Debug.Log("ground");
                currentState = moveState;
                moveState.TargetPosition = mousePosition;
                moveState.AttackAfter = false;
                moveState.EnterState(this);
            }
            else if (layer == LayerMask.NameToLayer("Enemy"))
            {
                Debug.Log("Enemy - zaatakuj");
            }
        }
    }
}
