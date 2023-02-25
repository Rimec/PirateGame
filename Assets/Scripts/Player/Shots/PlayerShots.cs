using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShots : MonoBehaviour
{
    [SerializeField] private float lifetime = 2f;// Tempo de vida do tiro
    [SerializeField] private int damage = 1;
    [SerializeField] private GameObject explosion;

    private void Start()
    {
        Destroy(gameObject, lifetime);// Destruir o tiro depois de um certo tempo
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            hitInfo.GetComponent<Enemy>().TakeDamage(damage);
        }
        Destroy(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}