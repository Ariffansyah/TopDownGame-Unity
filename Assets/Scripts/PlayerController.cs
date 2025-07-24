using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveDirection;
    private Vector2 ToggleRun;
    private Vector2 lastMoveDirection;
    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    private void Update()
    {
        if (DialogueManager.GetInstance().DialogueIsPlaying)
        {
            return;
        }

        moveDirection.x = InputManager.GetInstance().GetMoveDirection().x;
        moveDirection.y = InputManager.GetInstance().GetMoveDirection().y;
        moveDirection.Normalize();

        if (moveDirection != Vector2.zero)
        {
            lastMoveDirection = moveDirection;
        }

        animator.SetBool("isMoving", moveDirection != Vector2.zero);
        animator.SetFloat("moveX", lastMoveDirection.x);
        animator.SetFloat("moveY", lastMoveDirection.y);
    }

    private void FixedUpdate()
    {
        if (DialogueManager.GetInstance().DialogueIsPlaying)
        {
            animator.SetBool("isMoving", false);
            return;
        }
        rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
