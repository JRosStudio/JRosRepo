using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMoval : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        //Debug.Log(rb.velocity);
        if (rb.velocity.x >= 0.01f || rb.velocity.x <= -0.01f) {
            //Debug.Log("Rock moving horizontally");
        }
        if (rb.velocity.y >= 0.01f || rb.velocity.y <= -0.01f)
        {
            //Debug.Log("Rock falling");
        }
        if (rb.velocity.x <= 0.01f && rb.velocity.x >= -0.01f && rb.velocity.y <= 0.01f && rb.velocity.y >= -0.01f)
        {
            //Debug.Log("Rock Standing");
        }

    }
}
