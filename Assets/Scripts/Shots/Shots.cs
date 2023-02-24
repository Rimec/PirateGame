using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shots : MonoBehaviour
{
    public float lifetime = 2f;// Tempo de vida do tiro
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifetime);// Destruir o tiro depois de um certo tempo
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}