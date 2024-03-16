using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope_Fly : MonoBehaviour
{
    public Vector3 goal = new Vector3(0,0,0);
    public bool startMovement = false;
    public float speed = 5.0f;
    [SerializeField]
    GameObject rope;
    [SerializeField]
    GameObject ropeHook;
    bool deploying;
    public Vector3 playerY = new Vector3(0,0,0);

    private void Start()
    {
        playerY = GameObject.FindObjectOfType<Player>().transform.position;
    }
    void Update()
    {
        if (startMovement == true) {
           transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
            if (transform.position == goal) {
                DeployRope();
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

    public void DeployRope() {
        GameObject ropeHookInstance = Instantiate(ropeHook, goal, Quaternion.identity);
        deploying = true;
        float lastRopePos = ropeHookInstance.transform.position.y;
        int n = 1;
        Debug.Log("Last Rope Pos = " + lastRopePos + "n= " + n);


        do {
            if (lastRopePos > playerY.y+1)
            {
                Debug.Log("Last Rope Pos = " + lastRopePos + "n= " + n);
                GameObject ropeInstance1 = Instantiate(rope, new Vector3(goal.x, goal.y - n, goal.z), Quaternion.identity, ropeHookInstance.transform);
                lastRopePos = ropeInstance1.transform.position.y;
                n++;
            }
            else {
                deploying = false;
            }
  
        } while (deploying);

        Destroy(gameObject);
    }
}
