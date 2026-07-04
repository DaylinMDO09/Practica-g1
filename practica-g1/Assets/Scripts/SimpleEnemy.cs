using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float detectionRange = 3f;

    private Rigidbody2D rb;
    private Vector2 movement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            Vector2 direccion = (player.position - transform.position).normalized;
            movement = direccion * speed;
        }
        else
        {
            movement = Vector2.zero;
        }

        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }
}