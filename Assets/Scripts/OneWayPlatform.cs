using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private double counter = 0f;
    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Vertical") <= -0.9 && Input.GetButtonDown("Jump"))
        {
            counter = 0.45f;
            effector.rotationalOffset = 180;
        }
        if(counter <= 0.2f)
        {
            effector.rotationalOffset = 0;
        }

        if (counter > 0) {
            counter -= Time.deltaTime;
        }
    }


}
