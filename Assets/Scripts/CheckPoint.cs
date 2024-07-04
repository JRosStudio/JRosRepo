using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator checkpointAnimator;

    private void Start()
    {
        checkpointAnimator = gameObject.GetComponent<Animator>(); 
    }

    public void turnOffCheckPoint() {
        checkpointAnimator.SetInteger("State", 0);
    }

    public void tranistionToIddle() {
        checkpointAnimator.SetInteger("State", 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            collision.GetComponent<Player>().SetRespawnPos( this.transform.position.x, this.transform.position.y, gameObject);
            checkpointAnimator.SetInteger("State", 1);
            //code to start activation animation
        }
    }
}
