using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTipSafeward : MonoBehaviour
{
    private Player player;
    [SerializeField]
    private GameObject circle;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Ground") || collision.transform.CompareTag("Rope"))
        {
            circle.GetComponent<SpriteRenderer>().color = new Color(1,0,0,0.7f);
            player.ShootArrowBreak = false;
}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Ground") || collision.transform.CompareTag("Rope"))
        {
            circle.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
            player.ShootArrowBreak = true;
        }
    }
}
