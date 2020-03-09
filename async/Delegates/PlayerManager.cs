using Avendevs.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerManager : Singleton<PlayerManager>
{
    public int health = 100;

    // Forma A
    public delegate void HealthDelegate(int value);
    public HealthDelegate updateHealthDelegate;

    // Forma B
    public Action<int> UpdateHealth;

    private void Start() {
        play();
    }

    private void play() {
        StartCoroutine(simulate());
    }

    private void setPlayerHealth(int amount) {
        this.health = amount;

        if (UpdateHealth != null) {
            UpdateHealth(this.health);
        }
    }

    private IEnumerator simulate() {
        while (true) {
            setPlayerHealth(Random.Range(0,1000));
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }
}
