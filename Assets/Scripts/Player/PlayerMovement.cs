using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5f; // velocidade máxima
    [SerializeField] private float acceleration = 10f; // aceleração
    [SerializeField] private float deceleration = 10f; // desaceleração
    [SerializeField] private float rotationSpeed = 180f; // velocidade de rotação

    private Rigidbody2D rb;

    private float horizontalInput;
    private float currentSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Captura do Input
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        // Rotacionar o jogador
        transform.Rotate(Vector3.back * horizontalInput * rotationSpeed * Time.fixedDeltaTime);

        // Calcular a velocidade atual
        currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed * Input.GetAxisRaw("Vertical"), acceleration * Time.fixedDeltaTime);

        // Aplicar a velocidade ao rigidbody
        rb.velocity = transform.up * currentSpeed;

        // Desacelerar quando o jogador não está mais movendo
        if (Input.GetAxisRaw("Vertical") == 0f)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.fixedDeltaTime);
        }
    }
}
