using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope_Fly : MonoBehaviour
{
    public Vector3 goal = new Vector3(0, 0, 0);
    public bool startMovement = false;
    public float speed = 5.0f;
    [SerializeField]
    GameObject rope;
    [SerializeField]
    GameObject ropeHook;

    private int ropeLenght;


    private RopeDisplayer ropeDisp;
    bool deploying;
    public Vector3 playerY = new Vector3(0, 0, 0);
    private ResourceManager resourceManager;

    public void Awake()
    {
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        ropeDisp = GameObject.Find("Player").GetComponentInChildren<RopeDisplayer>();
        ropeLenght = ropeDisp.ropeLength;
        resourceManager.addRopes((ropeLenght + 1) * -1);
    }
    private void Start()
    {
        playerY = GameObject.FindObjectOfType<Player>().transform.position;
    }
    void Update()
    {
        if (startMovement == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
            if (transform.position == goal)
            {
                DeployRope();
            }
        }
    }

    public void StartMovement(Vector3 rope_goal)
    {
        goal = rope_goal;
        startMovement = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Rope")
        {
            Destroy(gameObject);
        }
    }

    public void DeployRope()
    {
        float lastRopePos = 0;
        GameObject ropeHookInstance = new GameObject();
        int n = 1;


        if (deploying == false)
        {
            ropeHookInstance = Instantiate(ropeHook, goal, Quaternion.identity);
            deploying = true;
            lastRopePos = ropeHookInstance.transform.position.y;
            //resourceManager.addRopes(-1);
        }

        for (int i = 1; i <= ropeLenght; i++) {
            //resourceManager.addRopes(-1);
             Instantiate(rope, new Vector3(goal.x, goal.y - i, goal.z), Quaternion.identity, ropeHookInstance.transform);
        }


       /* do
        {
            if (lastRopePos > playerY.y + 1 && resourceManager.getCurrentRopes() > 0)
            {
                resourceManager.addRopes(-1);
                GameObject ropeInstance1 = Instantiate(rope, new Vector3(goal.x, goal.y - n, goal.z), Quaternion.identity, ropeHookInstance.transform);
                lastRopePos = ropeInstance1.transform.position.y;
                n++;
            }
            else
            {
                deploying = false;
            }

        } while (deploying);*/

        Destroy(gameObject);
    }
}
