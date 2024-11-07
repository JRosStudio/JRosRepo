using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTipJumper : MonoBehaviour
{

    Player player;
    private bool jumpEnabled;

    [SerializeField]
    Transform tpPoint;

    private void Awake()
    {
        player = FindFirstObjectByType<Player>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "OneWayPlatform")
        {
            jumpEnabled = true;
        }
        else {
            jumpEnabled = false;
        }

        if (collision.gameObject.tag == "PlayerFeet" && jumpEnabled && player.inRope ) {
            Debug.Log("TP AT TIP");
            player.transform.position = tpPoint.position;
        }
    }
}
