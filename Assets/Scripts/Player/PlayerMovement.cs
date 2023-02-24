using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // velocidade de movimento do personagem
    [SerializeField] private float rotationSpeed = 200f; // velocidade de rotação do personagem
    [SerializeField] private bool allowBackwardsMovement = true; // indica se o personagem pode se mover para trás

    private Rigidbody2D rb2D; // referência ao componente Rigidbody2D

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // obter referência ao componente Rigidbody2D
    }

    void Update()
    {
        // Obter entrada do usuário para mover o personagem para frente ou para trás
        float moveInput = Input.GetAxisRaw("Vertical");

        // Verificar se o valor de entrada é maior do que zero (para mover para frente)
        // ou menor do que zero (para mover para trás), dependendo da variável allowBackwardsMovement
        if ((moveInput > 0f || (allowBackwardsMovement && moveInput < 0f)))
        {
            // Calcular a direção do movimento do personagem
            Vector2 moveDirection = transform.up * moveInput;

            // Obter entrada do usuário para rotacionar o personagem
            float rotateInput = Input.GetAxisRaw("Horizontal");

            // Rotacionar o personagem
            transform.Rotate(Vector3.forward * -rotateInput * rotationSpeed * Time.deltaTime);

            // Aplicar a velocidade de movimento ao personagem
            rb2D.velocity = moveDirection * moveSpeed;
        }
        else
        {
            // Se o valor de entrada for zero ou não permitido, o personagem não se move
            rb2D.velocity = Vector2.zero;

            // Obter entrada do usuário para rotacionar o personagem
            float rotateInput = Input.GetAxisRaw("Horizontal");

            // Rotacionar o personagem
            transform.Rotate(Vector3.forward * -rotateInput * rotationSpeed * Time.deltaTime);
        }
    }
}

