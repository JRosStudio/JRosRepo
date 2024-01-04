using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Roca : MonoBehaviour
{
    [SerializeField]
    private int health;

    [SerializeField]
    private TMP_Text text_health;

    private void Update()
    {
        text_health.text = health.ToString();
    }

    public void takeDamage (int damage) {
        Debug.Log("Recibe Daño");
        health -= damage;

        if (health <= 0) {
            Debug.Log("Muere");
            Destroy(gameObject);
        }
    }
}
