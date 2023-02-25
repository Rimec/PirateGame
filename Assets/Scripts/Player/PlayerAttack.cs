using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // prefab do tiro do personagem
    [SerializeField] private float bulletSpeed = 10f; // velocidade do tiro do personagem
    [SerializeField] private float timeBetweenShots = 0.5f; // tempo mínimo entre cada tiro do personagem
    [SerializeField] private bool canShoot = true; // indica se o personagem pode atirar

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            // Chamar a função de ataque único frontal
            ShootSingleBullet();
        }

        if (Input.GetButtonDown("Fire2") && canShoot)
        {
            // Chamar a função de ataque triplo lateral
            ShootTripleBullets();
        }
    }

    private void ShootSingleBullet()
    {
        // Criar uma nova instância do prefab do tiro
        GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.up * 0.5f, transform.rotation);

        // Obter a direção para a qual o personagem está apontando
        Vector2 bulletDirection = transform.up;

        // Adicionar uma força para mover o tiro na direção correta
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;

        // Impedir que o personagem atire novamente imediatamente
        canShoot = false;
        Invoke("EnableShooting", timeBetweenShots);
    }

    private void ShootTripleBullets()
    {
        // Calcular a posição inicial para os tiros laterais
        Vector2 startingPosition = transform.position - transform.right * -0.5f;
        Vector2 startingPosition2 = transform.position - transform.right * 0.5f;

        // Criar 3 novas instâncias do prefab do tiro
        GameObject bullet1 = Instantiate(bulletPrefab, startingPosition, transform.rotation);
        GameObject bullet2 = Instantiate(bulletPrefab, startingPosition + (Vector2)transform.up * 0.5f, transform.rotation);
        GameObject bullet3 = Instantiate(bulletPrefab, startingPosition - (Vector2)transform.up * 0.5f, transform.rotation);
        GameObject bullet4 = Instantiate(bulletPrefab, startingPosition2, transform.rotation);
        GameObject bullet5 = Instantiate(bulletPrefab, startingPosition2 + (Vector2)transform.up * 0.5f, transform.rotation);
        GameObject bullet6 = Instantiate(bulletPrefab, startingPosition2 - (Vector2)transform.up * 0.5f, transform.rotation);

        // Obter a direção para a qual o personagem está apontando
        Vector2 bulletDirection = transform.right;

        // Adicionar uma força para mover cada tiro na direção correta
        bullet1.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;
        bullet2.GetComponent<Rigidbody2D>().velocity = (bulletDirection + (Vector2)transform.right * 0.5f).normalized * bulletSpeed;
        bullet3.GetComponent<Rigidbody2D>().velocity = (bulletDirection - (Vector2)transform.right * 0.5f).normalized * bulletSpeed;
        bullet4.GetComponent<Rigidbody2D>().velocity = -bulletDirection * bulletSpeed;
        bullet5.GetComponent<Rigidbody2D>().velocity = (-bulletDirection + (Vector2)transform.right * 0.5f).normalized * bulletSpeed;
        bullet6.GetComponent<Rigidbody2D>().velocity = (-bulletDirection - (Vector2)transform.right * 0.5f).normalized * bulletSpeed;


        // Impedir que o personagem atire novamente imediatamente
        canShoot = false;
        Invoke("EnableShooting", timeBetweenShots);
    }

    private void EnableShooting()
    {
        // Permitir que o personagem atire novamente
        canShoot = true;
    }
}
