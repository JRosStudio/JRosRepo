using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheckRockDetector : MonoBehaviour
{
    public bool RockDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rock")) {
            RockDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Rock"))
        {
            RockDetected = false;
        }
    }
}
