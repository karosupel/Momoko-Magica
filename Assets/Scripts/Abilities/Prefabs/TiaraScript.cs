using UnityEngine;

public class TiaraScript : MonoBehaviour
{
    public Vector2 direction;
    public bool comeBack;
    public float speed;

    private Rigidbody2D rb;
    private Transform returnTarget;
    private float range;
    private float returnSpeed;
    private float distanceTravelled;
    private Vector2 previousPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    public void SetStats(Transform player, Vector2 targetPosition, float distance, float startSpeed)
    {
        if (player == null)
        {
            Debug.LogError("Tiara requires a player target.", this);
            Destroy(gameObject);
            return;
        }

        if (startSpeed <= 0f)
        {
            Debug.LogError("Tiara speed must be greater than zero.", this);
            Destroy(gameObject);
            return;
        }

        returnTarget = player;
        direction = targetPosition - rb.position;
        range = Mathf.Max(0f, distance);
        speed = startSpeed;
        returnSpeed = startSpeed * 1.5f;
        distanceTravelled = 0f;
        previousPosition = rb.position;
        comeBack = false;
    }

    void FixedUpdate()
    {
        if (returnTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 position = rb.position;
        if (!comeBack)
        {
            distanceTravelled += Vector2.Distance(position, previousPosition);

            Vector2 toPlayer = (Vector2)returnTarget.position - position;
            if (direction.sqrMagnitude <= 0.0001f || distanceTravelled + toPlayer.magnitude >= range)
                comeBack = true;
        }

        previousPosition = position;

        if (comeBack)
        {
            Vector2 toPlayer = (Vector2)returnTarget.position - position;
            if (toPlayer.sqrMagnitude <= 0.01f)
            {
                rb.linearVelocity = Vector2.zero;
                Destroy(gameObject);
                return;
            }

            rb.linearVelocity = toPlayer.normalized * returnSpeed;
        }
        else
        {
            rb.linearVelocity = direction.normalized * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && comeBack)
            Destroy(gameObject);
    }
}
