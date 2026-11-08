using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private Animator animator;
    private Transform attackPoint;
    private LayerMask enemyLayer;
    private float attackRange;
    private float damageLight;
    private float damageHeavy;
    private float attackCooldown = 0.5f;
    private int comboCount;
    private float comboCooldown;
    
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Attack");
        animator = player.GetComponent<Animator>();
        attackPoint = player.attackPoint;
        enemyLayer = player.enemyLayer;
        attackRange = player.AttackRange;
        damageLight = player.DamageLight;
        damageHeavy = player.DamageHeavy;
        attackCooldown = player.AttackCooldown;
        comboCooldown = player.ComboCooldown;

        if (Time.time - player.LastAttackTime < attackCooldown)
        {
            player.SwitchState(player.idleState);
        }

        if (Time.time - player.LastComboTime > comboCooldown)
        {
            Debug.Log("combo reset");
            player.ComboCount = 1;
        }
        else
        {
            if (player.ComboCount < 3)
            {
                player.ComboCount++;
            }
            else
            {
                player.ComboCount = 1;
            }
        }
        player.LastAttackTime = Time.time;
        player.LastComboTime = Time.time;

        AttackEnemies();

        animator.SetFloat("FacingDirectionX", player.Direction.x);
        animator.SetFloat("FacingDirectionY", player.Direction.y);

        animator.SetInteger("ComboCount", player.ComboCount);
        animator.SetTrigger("Attack");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        
    }

    private void AttackEnemies()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(new Vector2(attackPoint.transform.position.x, attackPoint.transform.position.y), attackRange, enemyLayer);
        foreach (Collider2D enemy in enemies)
        {
            if(comboCount > 2)
                enemy.gameObject.GetComponent<HealthScript>().GetHit(damageHeavy);
            else
                enemy.gameObject.GetComponent<HealthScript>().GetHit(damageLight);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(new Vector2(attackPoint.transform.position.x, attackPoint.transform.position.y), attackRange);
    }
}
