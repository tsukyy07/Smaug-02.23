using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovimentoJogador : MonoBehaviour
{
    public float velocidade = 5f;
    public float forcaPulo = 10f;

    public float alturaAgachamento = 0.5f;
    private bool noChao = false;
    public bool isJumping;


    private Rigidbody2D rb;
    private bool chekMoviment;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 5f;
        rb.gravityScale = 2f;
    }
    void Update()
    {
        // Movimento para a Direita e Esquerda
        float movimentoHorizontal = Input.GetAxis("Horizontal");
        Vector3 movimento = new Vector3(movimentoHorizontal, 0f, 0f);
        transform.position += movimento * Time.deltaTime * velocidade;

        if (movimentoHorizontal != 0)
        {
            Debug.Log("Está andando!");
            chekMoviment = true;

        }
        else
        {
            chekMoviment = false;
        }


        // Pulo

          if (Input.GetButtonDown("Jump") && !isJumping)
          {
            rb.AddForce(new Vector2(0f, forcaPulo), ForceMode2D.Impulse);
          }



        // Agachar
        if (Input.GetButtonDown("Crouch"))
        {
            transform.localScale = new Vector3(1f, alturaAgachamento, 1f);
        }

        if (Input.GetButtonUp("Crouch"))
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }


}

