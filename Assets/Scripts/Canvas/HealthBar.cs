using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private GameObject target;
    private RectTransform rectTransform;
    private float maxHealth;

    [SerializeField] private Slider slider;
    // Start is called before the first frame update
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }


    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            rectTransform.anchoredPosition = target.transform.localPosition;
        }
    }
    public void SetTarget(GameObject _target)
    {
        target = _target;
    }
    public void SetMaxHealth(float _maxHealth)
    {
        if (maxHealth == 0.0f)
        {
            maxHealth = _maxHealth;
            slider.maxValue = maxHealth;
        }
    }
    public void TakeDamage(float currentHealth)
    {
        slider.value = currentHealth;
    }
}
