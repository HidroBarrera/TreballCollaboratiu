using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    // Velocitat de moviment del jugador en el mapa
    [SerializeField] float speed;
    [Range(1, 500)] public float potenciaSalto;

    bool isJumping = false;

    private Controls playerInput;
    private Rigidbody2D rb2d;
    // Inicialitzes un vector2
    Vector2 movementInput;
    private void Awake()
    {
        playerInput = new Controls();
        playerInput.Overoll.Enable();
    }

    // Es cridat despres del primer frame
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Es crida per cada frame
    void Update()
    {
        movementInput = playerInput.Overoll.Moviment.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        if (Input.GetButton("Jump") && !isJumping)
        {
            //Le aplico la fuerza de salto
            rb2d.AddForce(Vector2.up * potenciaSalto);
            //Digo que está saltando (para que no pueda volver a saltar)
            isJumping = true;
        }

        rb2d.velocity = new Vector2(movementInput.x * speed, rb2d.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Si el jugador colisiona con un objeto con la etiqueta suelo
        if (other.gameObject.CompareTag("Suelo"))
        {
            //Digo que no está saltando (para que pueda volver a saltar)
            isJumping = false;
            //Le quito la fuerza de salto remanente que tuviera
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Death"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
