using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Image _healthBarFill;
    private TextMeshProUGUI _healthBarText;
    private Character _character;
    
    private void Awake()
    {
        _healthBarFill = GetComponentInChildren<Image>();
        _healthBarText = GetComponentInChildren<TextMeshProUGUI>();
        _character = GetComponentInParent<Character>();
    }

    private void Start()
    {
        OnHealthChange();
    }

    private void OnEnable()
    {
        Character.OnHealthChange += OnHealthChange;
    }

    private void OnDisable()
    {
        Character.OnHealthChange -= OnHealthChange;
    }
    
    private void OnHealthChange()
    {
        UpdateHealthBarFill(_character.HealthPercentage);
        UpdateHealthBarText(_character.Health, _character.MaxHealth);
    }
    
    private void UpdateHealthBarFill(float percentage)
    {
        _healthBarFill.fillAmount = percentage;
    }
    
    private void UpdateHealthBarText(int currentHealth, int maxHealth)
    {
        _healthBarText.text = $"{currentHealth} / {maxHealth}";
    }
}
