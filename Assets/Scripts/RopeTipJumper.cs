using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTipJumper : MonoBehaviour
{

    Player player;
    private bool jumpEnabled = false;

    [SerializeField]
    Transform tpPoint;

    private void Awake()
    {
        player = FindFirstObjectByType<Player>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("OneWayPlatform"))
        {
            jumpEnabled = true;
        }

        if (collision.CompareTag("Ground"))
        {
            jumpEnabled = false;
        }

        if (collision.CompareTag("PlayerFeet") && jumpEnabled && player.inRope)
        {
            player.transform.position = tpPoint.position;
            player.inRope = false;
        }
    }
}
