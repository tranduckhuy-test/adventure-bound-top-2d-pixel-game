using UnityEngine;

public class Slime : Enemy
{
    private Rigidbody2D myRigidbody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameObject.FindWithTag("Player").transform;
    }



    private void Update()
    {
        CheckDistance();
        animator.SetBool("isMoving", isMoving);

        if (isHit)
        {
            animator.SetBool("isHit", true);
            if (isDead)
            {
                animator.SetTrigger("dead");
                StartCoroutine(Die());
            } else
            {
                isHit = false;
            }
        }
        else
        {
            animator.SetBool("isHit", false);
        }
    }

    private void CheckDistance()
    {
        var distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRange && distance > attackRadius)
        {
            var temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            myRigidbody.MovePosition(temp);
            isMoving = true;

            if (target.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            isMoving = false;
        }
    }
}
