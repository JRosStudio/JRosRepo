using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope_Fly : MonoBehaviour
{
    public Vector3 goal = new Vector3(0,0,0);
    public bool startMovement = false;
    public float speed = 5.0f;
    void Update()
    {
        if (startMovement == true) {
           transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
            if (transform.position == goal) {
                Destroy(gameObject);
            }
        }
    }

    public void StartMovement( Vector3 rope_goal) {
        goal = rope_goal;
        startMovement = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Rope") {
            Destroy(gameObject);
        }
    }
}
