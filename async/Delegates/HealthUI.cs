using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Text valueUI;

    private void Awake() {
        PlayerManager.Instance.UpdateHealth += UpdateHealth;
    }

    private void OnDestroy() {
        PlayerManager.Instance.UpdateHealth -= UpdateHealth;
    }

    public void UpdateHealth(int health) {
        //valueUI.text = PlayerManager.Instance.health.ToString();
        valueUI.text = health.ToString();
    }
}
