using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_projectile : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") {
            Debug.Log("ROCK HIT GROUND");
            player.rock = null;
            Destroy(gameObject);
            
        }
        if (collision.gameObject.tag == "Rock")
        {
            Debug.Log("ROCK HIT ROCK");
            player.rock = null;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            Debug.Log("ROCK HIT WATER");
            player.rock = null;
            Destroy(gameObject);
        }
    }
}
