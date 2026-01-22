using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTp : MonoBehaviour
{
    [SerializeField] GameObject tpPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null && player.alive)
            {
                player.transform.position = new Vector3(
                    tpPoint.transform.position.x,
                    player.transform.position.y,
                    player.transform.position.z
                );
            }
        }
    }

    }
