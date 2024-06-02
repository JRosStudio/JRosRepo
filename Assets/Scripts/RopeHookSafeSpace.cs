using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeHookSafeSpace : MonoBehaviour
{

    private Player player;
    [SerializeField] 
    GameObject circle;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Ground") || collision.transform.CompareTag("Rope"))
        {
            Debug.Log("ENTRA");
            player.ShootArrowSecurityBreak = false;
            circle.GetComponent<SpriteRenderer>().color = new Color (1,0,0,0.7f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Ground") || collision.transform.CompareTag("Rope"))
        {
            Debug.Log("SALE");
            player.ShootArrowSecurityBreak = true;
            circle.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
        }
    }

}
