using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemigo : MonoBehaviour
{
    [SerializeField]
    private int health;



    public void takeDamage (int damage) {
        //Debug.Log("Recibe Daño");
        health -= damage;

        if (health <= 0) {
            //Debug.Log("Muere");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
