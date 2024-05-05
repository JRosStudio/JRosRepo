using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePreviewCutter : MonoBehaviour
{
    private RopeDisplayer ropeDisp;

    private void Awake()
    {
        ropeDisp = GameObject.Find("Player").GetComponentInChildren<RopeDisplayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Ground") || collision.transform.CompareTag("Rope")) {
            Debug.Log("Rope In Ground!");
            ropeDisp.RopeCutter(gameObject);
        }
    }
}
