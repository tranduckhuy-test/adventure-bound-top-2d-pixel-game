using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Animator animator;
    private bool isMoving;
    private bool isLockedMovement;
    public LayerMask solidObjectsLayer;

    private void Awake()
    {
        playerControls = new PlayerControls();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        if (!isMoving)
        {
            movement = playerControls.Player.Move.ReadValue<Vector2>();

            if (!isLockedMovement && movement != Vector2.zero)
            {
                animator.SetFloat("moveX", movement.x);
                animator.SetFloat("moveY", movement.y);

                var targetPos = transform.position;
                targetPos.x += movement.x;
                targetPos.y += movement.y;

                AdjustTargetPositionToColliderEdge(ref targetPos);
                StartCoroutine(MoveToPosition(targetPos));
            }
        }

        animator.SetBool("isMoving", isMoving);

        if (playerControls.Player.Attack.triggered)
        {
            OnAttack();
        }
    }

    private void AdjustTargetPositionToColliderEdge(ref Vector3 targetPos)
    {
        Vector3 direction = (targetPos - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPos);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, solidObjectsLayer);

        if (hit.collider != null)
        {
            targetPos = hit.point - (Vector2)direction * 0.2f;
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPos)
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, targetPos) > Mathf.Epsilon)
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            transform.position = newPos;

            yield return null;
        }
        isMoving = false;
    }

    private void OnAttack()
    {
        Debug.Log("Attack!");
        animator.SetTrigger("attack");
    }

    private void LockMovement()
    {
        isLockedMovement = true;
    }

    private void UnlockMovement()
    {
        isLockedMovement = false;
    }
}
