using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [Header("Enemy Atributes")]
    [Space(10)]
    [SerializeField] private float moveSpeed = 2f; // Velocidade de movimento
    [SerializeField] private float maxHealth = 5.0f;
    [SerializeField] private float damage = 5.0f;

    private float currentHealth;
    protected Rigidbody2D rb;
    protected GameObject player;

    [SerializeField] private GameObject healthBarCanvas;
    private HealthBar healthBar;

    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip animation;
    [SerializeField] private GameObject explosion;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        if (player == null)
        {
            return;
        }

        // Move na direção do jogador
        Vector3 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        // Rotaciona em direção ao jogador
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            HealthBarConfig();
            DefineAnimation();
            Explode();
        }
        else
        {
            HealthBarConfig();
            DefineAnimation();
        }
    }

    private void HealthBarConfig()
    {
        if (healthBar == null)
        {
            if (Instantiate(healthBarCanvas, this.transform.position, Quaternion.identity).TryGetComponent<HealthBar>(out healthBar))
            {
                healthBar.SetTarget(this.gameObject);
                healthBar.SetMaxHealth(MaxHealth);
                healthBar.TakeDamage(CurrentHealth);
            }
        }
        else
        {
            healthBar.TakeDamage(CurrentHealth);
        }
    }

    public virtual void Explode()
    {
        // Destroi o inimigo
        Instantiate(explosion, transform.position, Quaternion.identity);
        moveSpeed = 0.0f;
        Destroy(healthBar.gameObject, 1.0f);
        Destroy(gameObject, 1.0f);
    }

    private void DefineAnimation()
    {
        float _currentHealth = currentHealth / maxHealth * 100.0f;
        if ((_currentHealth <= 100.0f) && (_currentHealth >= 75.0f))
        {
            animator.Play(animation.name, -1, 0f / 30f);
        }
        else if (_currentHealth < 75.0f && _currentHealth >= 40.0f)
        {
            animator.Play(animation.name, -1, 10f / 30f);
        }
        else if (_currentHealth < 40.0f && _currentHealth >= 1.0f)
        {
            animator.Play(animation.name, -1, 20f / 30f);
        }
        else
        {
            animator.Play(animation.name, -1, 30f / 30f);
        }
    }

    public float MoveSpeed { get => moveSpeed; }
    public float MaxHealth { get => maxHealth; }
    public float CurrentHealth { get => currentHealth; }
    public float Damage { get => damage; }
}
