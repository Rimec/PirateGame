using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject healthBarCanvas;
    [SerializeField] private GameObject explosion;
    [SerializeField] private Animator animator;
    private HealthBar healthBar;
    public void TakeDamage(float damage)
    {
        GameManager.instance.PlayerTakeDamage(damage);
        float currentHealth = GameManager.instance.PlayerCurrentHealth;
        if (currentHealth <= 0.0f)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
        HealthBarConfig(currentHealth);
        DefineAnimation(currentHealth);
    }

    private void HealthBarConfig(float currentHealth)
    {
        if (healthBar == null)
        {
            if (Instantiate(healthBarCanvas, this.transform.position, Quaternion.identity).TryGetComponent<HealthBar>(out healthBar))
            {
                healthBar.SetTarget(this.gameObject);
                healthBar.SetMaxHealth(GameManager.instance.PlayerHealth);
                healthBar.TakeDamage(currentHealth);
            }
        }
        else
        {
            healthBar.TakeDamage(currentHealth);
        }
    }
    private void DefineAnimation(float currentHealth)
    {
        float _currentHealth = currentHealth / GameManager.instance.PlayerHealth * 100.0f;
        if ((_currentHealth <= 100.0f) && (_currentHealth >= 75.0f))
        {
            animator.Play("PlayerAnimation", -1, 0f / 30f);
        }
        else if (_currentHealth < 75.0f && _currentHealth >= 40.0f)
        {
            animator.Play("PlayerAnimation", -1, 10f / 30f);
        }
        else if (_currentHealth < 40.0f && _currentHealth >= 1.0f)
        {
            animator.Play("PlayerAnimation", -1, 20f / 30f);
        }
        else
        {
            animator.Play("PlayerAnimation", -1, 30f / 30f);
        }
    }
}
