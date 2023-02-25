using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] enemys = new GameObject[0];
    [SerializeField] float maxYPosition = 10.0f;
    [SerializeField] float minYPosition = -10.0f;
    [SerializeField] float maxXPosition = 10.0f;
    [SerializeField] float minXPosition = -10.0f;
    private Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("CreateEnemy", 1.0f, GameManager.instance.SpawnTime);
    }
    private void CreateEnemy()
    {
        if (enemys.Length > 0)
        {
            Vector3 position = new Vector3(transform.position.x + Random.Range(minXPosition, maxXPosition), transform.position.y + Random.Range(minYPosition, maxYPosition), 0.0f);
            Instantiate(enemys[Random.Range(0, enemys.Length)], position, Quaternion.identity);
        }
    }
    private void Update()
    {
        transform.position = player.transform.position;
    }
}
