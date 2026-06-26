using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class movimiento : MonoBehaviour
{
    [Header("Movimiento")]
    public float mover = 5f;
    public float salto = 8f;

    [Header("UI")]
    public TMP_Text contadorJuego;

    private Rigidbody2D rg2d;
    private Animator animator;

    private float movimientoX;
    private bool saltar;
    private bool isGrounded;
    private Vector3 escalaOriginal;

    private int manzanas = 0;
    private int bananas = 0;

    private bool recolectando = false;

    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        escalaOriginal = transform.localScale;

        ActualizarUI();
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

        animator.SetBool("Correr", movimientoX != 0);
        animator.SetBool("Saltar", !isGrounded);

        Girar();
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

    private void Girar()
    {
        if (movimientoX > 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(escalaOriginal.x),
                escalaOriginal.y,
                escalaOriginal.z
            );
        }
        else if (movimientoX < 0)
        {
            transform.localScale = new Vector3(
                -Mathf.Abs(escalaOriginal.x),
                escalaOriginal.y,
                escalaOriginal.z
            );
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (recolectando) return;

        if (other.CompareTag("Collectible-Manzana"))
        {
            recolectando = true;
            manzanas++;
            StartCoroutine(Recolectar(other.gameObject));
        }
        else if (other.CompareTag("Collectible-Banana"))
        {
            recolectando = true;
            bananas++;
            StartCoroutine(Recolectar(other.gameObject));
        }

        ActualizarUI();
    }

    private IEnumerator Recolectar(GameObject objeto)
    {
        Animator anim = objeto.GetComponent<Animator>();

        if (anim != null)
        {
            anim.SetTrigger("Collect");
        }

        yield return new WaitForSeconds(0.35f);

        recolectando = false;
        Destroy(objeto);
    }

    private void ActualizarUI()
    {
        if (contadorJuego != null)
        {
            contadorJuego.text = "Manzanas: " + manzanas + " | Bananas: " + bananas;
        }
    }
}