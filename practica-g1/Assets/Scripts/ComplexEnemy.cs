using UnityEngine;

public class ComplexEnemy : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 3.5f;
    public float speed = 2f;
    private bool facingRight = true;
    public float jumpForce = 5f;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            if (direction.x > 0 && !facingRight)
                Flip();
            else if (direction.x < 0 && facingRight)
                Flip();

            animator.SetBool("isWalking", true);

            transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        void Flip()
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
