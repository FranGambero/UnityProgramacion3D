using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider healthSlider;

    private int maxHealthValue = 1000;

    private void Awake() {
        PlayerManager.Instance.UpdateHealth += updateHealthSlider;
    }

    private void OnDestroy() {
        PlayerManager.Instance.UpdateHealth -= updateHealthSlider;
    }

    public void updateHealthSlider(int health) {
        float normalizeHealth = health / (float)maxHealthValue;
        healthSlider.normalizedValue = normalizeHealth;
    }
}
