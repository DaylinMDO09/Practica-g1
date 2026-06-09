using UnityEngine;
using UnityEngine.InputSystem;

public class movimiento : MonoBehaviour
{
    public float mover = 5f;
    public float salto = 8f;

    private Rigidbody2D rg2d;
    private float movimientoX;
    private bool saltar;
    private bool isGrounded;

    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movimientoX = 0f;

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            movimientoX = mover;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            movimientoX = -mover;

        if ((Keyboard.current.wKey.wasPressedThisFrame ||
             Keyboard.current.upArrowKey.wasPressedThisFrame) &&
             isGrounded)
        {
            saltar = true;
        }
    }

    void FixedUpdate()
    {
        rg2d.linearVelocity = new Vector2(
            movimientoX,
            rg2d.linearVelocity.y
        );

        if (saltar)
        {
            rg2d.linearVelocity = new Vector2(
                rg2d.linearVelocity.x,
                salto
            );

            saltar = false;
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}