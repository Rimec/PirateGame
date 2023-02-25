using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShots : MonoBehaviour
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
        if (hitInfo.gameObject.CompareTag("Player"))
        {
            hitInfo.gameObject.GetComponent<Player>().TakeDamage(damage);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (!hitInfo.gameObject.CompareTag("Enemy"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}