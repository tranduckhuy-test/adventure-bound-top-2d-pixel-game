using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public int baseAttack = 1;
    public int health = 3;
    public string enemyName = "Enemy";
    public Transform target;
    public float chaseRange = 5.0f;
    public float attackRadius = 1.0f;
    public Transform homePosition;
    protected bool isMoving;
    protected bool isHit;
    protected bool isDead;

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        isHit = true;
        if (health <= 0)
        {
            isDead = true;
        }
    }

    protected IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
