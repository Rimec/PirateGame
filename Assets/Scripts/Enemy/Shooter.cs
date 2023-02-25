using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    [Header("Shooter Atributes")]
    [Space(10)]
    [SerializeField] private float distanceToFire = 5.0f;
    [SerializeField] private GameObject bulletPrefab; // prefab do tiro do personagem
    [SerializeField] private float bulletSpeed = 10f; // velocidade do tiro do personagem
    [SerializeField] private float timeBetweenShots = 2.0f; // tempo mínimo entre cada tiro do personagem

    [SerializeField] private float currentTimeToShoot = 0.0f;
    protected override void Move()
    {
        if (player == null)
        {
            return;
        }
        Vector3 direction = (player.transform.position - transform.position).normalized;
        if (Vector3.Distance(player.transform.position, transform.position) >= distanceToFire)
        {
            // Move na direção do jogador
            rb.velocity = direction * MoveSpeed;
        }
        else
        {
            rb.velocity = Vector3.zero;
            currentTimeToShoot += Time.fixedDeltaTime;
            if (currentTimeToShoot >= timeBetweenShots)
            {
                Fire();
                currentTimeToShoot = 0.0f;
            }
        }
        // Rotaciona em direção ao jogador
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void Fire()
    {
        // Criar uma nova instância do prefab do tiro
        GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.up * 0.5f, transform.rotation);

        // Obter a direção para a qual o personagem está apontando
        Vector2 bulletDirection = transform.up;

        // Adicionar uma força para mover o tiro na direção correta
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;
    }
}
