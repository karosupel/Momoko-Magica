using System.Collections.Generic;
using UnityEngine;

public class WhirlScript : MonoBehaviour
{
    CircleCollider2D triggerCollider;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float pullAcceleration = 8f;
    [SerializeField] private float pullAccelerationSlow = 0.1f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float damageInterval = 0.5f;

    private readonly HashSet<Rigidbody2D> enemies = new();
    private float damageTimer = 0.5f;
    public float DamageTimer {get {return damageTimer;} set{damageTimer = value;}}

    void Awake()
    {
        triggerCollider = GetComponent<CircleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((enemyLayer.value & (1 << other.gameObject.layer)) == 0)
            return;

        if (other.attachedRigidbody != null)
            enemies.Add(other.attachedRigidbody);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.attachedRigidbody != null)
            enemies.Remove(other.attachedRigidbody);
    }

    private void FixedUpdate()
    {
        Vector2 center = transform.position;

        foreach (Rigidbody2D enemy in enemies)
        {
            if (enemy == null)
                continue;

            Vector2 toCenter = center - enemy.worldCenterOfMass;
            if (toCenter.sqrMagnitude > 0.1f)
            {
                enemy.AddForce(toCenter.normalized * pullAcceleration * enemy.mass);
                pullAcceleration -= pullAccelerationSlow;
            }
            if (toCenter.sqrMagnitude <= 0.1f)
            {
                enemy.linearVelocity = Vector2.zero;
            }
        }

        damageTimer += Time.fixedDeltaTime;
        if (damageTimer < damageInterval)
            return;

        damageTimer -= damageInterval;

        foreach (Rigidbody2D enemy in enemies)
        {
            if (enemy != null && enemy.TryGetComponent(out HealthScript health))
                health.GetHit(damage);
        }
    }

    public void SetStats(float range, float pullAccelaration, float pullAccelerationRate, float damage, float damageInterval)
    {
        triggerCollider.radius = range;
        this.pullAcceleration = pullAccelaration;
        this.pullAccelerationSlow = pullAccelerationRate;
        this.damage = damage;
        this.damageInterval = damageInterval;
    }
}
