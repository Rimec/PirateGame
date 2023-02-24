using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Velocidade de movimento
    [SerializeField] private float rotationSpeed = 100f; // Velocidade de rotação
    [SerializeField] private int currentHealth = 0;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Movimenta o inimigo para frente
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // Rotaciona o inimigo para a direção em que ele está se movendo
        Vector3 direction = GetComponent<Rigidbody2D>().velocity.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle - 90, Vector3.forward), rotationSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Explode();
        }
    }
    public virtual void Explode()
    {
        // Destroi o inimigo
        Destroy(gameObject);
    }
}
