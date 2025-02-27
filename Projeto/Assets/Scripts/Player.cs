using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidade = 10f;    // A velocidade de movimento
    public float focaPulo = 10f;     // A força do pulo

    public bool noChao = false;      // Verifica se o jogador está no chão
    public bool andando = false;     // Determina se o jogador está andando

    private Rigidbody2D _rigidbody2D;  // Referência ao Rigidbody2D do jogador
    private SpriteRenderer _spriteRenderer; // Referência ao SpriteRenderer para inverter a direção do sprite
    private Animator _animator; // Referência ao Animator para controlar as animações

    // Start é chamado antes do primeiro frame
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
    }

    // Verifica se o jogador está tocando o chão
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "chao")
        {
            noChao = true;
        }
    }

    // Verifica quando o jogador sai do chão
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "chao")
        {
            noChao = false;
        }
    }

    // Update é chamado a cada frame
    void Update()
    {
        andando = false;  // Inicializa a variável de "andando" como false no início

        float moveInput = 0f; // Variável para controlar o movimento

        // Movimentação para a esquerda
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1f;  // Define que o jogador vai para a esquerda
            _spriteRenderer.flipX = true;  // Inverte o sprite para a esquerda
        }

        // Movimentação para a direita
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1f;  // Define que o jogador vai para a direita
            _spriteRenderer.flipX = false;  // Inverte o sprite para a direita
        }

        // Aplica o movimento horizontal ao jogador
        _rigidbody2D.velocity = new Vector2(moveInput * velocidade, _rigidbody2D.velocity.y);

        // Se o jogador está se movendo e está no chão, ativa a animação de "andando"
        if (moveInput != 0 && noChao)
        {
            andando = true;
        }

        // Verifica se o jogador apertou a tecla de pulo e está no chão
        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            _rigidbody2D.AddForce(new Vector2(0, 1) * focaPulo, ForceMode2D.Impulse);  // Aplica a força para o pulo
        }

        // Atualiza a animação no Animator
        _animator.SetBool("Andando", andando);  // Controla a animação de "Andando"
    }
}