using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class EnemyBaseScript : MonoBehaviour
{
    bool isAttackingSchool = false;
    [SerializeField] LayerMask schoolLayer;
    [SerializeField] float speed;
    private Vector2 direction = Vector2.down;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAttackingSchool==false)
        {
            MoveFoward();
        }
        else
        {
            AttackSchool();
        }
    }

    void MoveFoward()
    {
        rb.linearVelocity = direction * speed;
    }

    void AttackSchool()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == schoolLayer)
        {
            isAttackingSchool = true;
        }
    }
}
