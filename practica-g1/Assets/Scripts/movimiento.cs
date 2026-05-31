using UnityEngine;

public class movimiento : MonoBehaviour
{
    public float mover = 2;

    public float salto = 3;

    Rigidbody2D rg2d;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            rg2d.linearVelocity = new Vector2(mover, rg2d.linearVelocity.y);
        }
        else if (Input.GetKey("left") || Input.GetKey("a"))
        {
            rg2d.linearVelocity = new Vector2(-mover, rg2d.linearVelocity.y);
        }
        else
        {
            rg2d.linearVelocity = new Vector2(0, rg2d.linearVelocity.y);
        }
        if ((Input.GetKey("up") || Input.GetKey("w")) && Check.isGrounded)
        {
            rg2d.linearVelocity = new Vector2(rg2d.linearVelocity.x, salto);
        }


    }
}
