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

    Player player;

    private void Start()
    {
        player = GameObject.FindAnyObjectByType<Player>();
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
        Debug.Log(goal + " "+ player.transform.position);
        if (ropeHookInstance.transform.position.y < player.transform.position.y) {
            GameObject ropeInstance = Instantiate(ropeHook, new Vector3(goal.x, goal.y+1 , goal.z), Quaternion.identity, ropeHookInstance.transform);
        }
        Destroy(gameObject);
    }
}
