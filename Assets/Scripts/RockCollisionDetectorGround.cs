using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollisionDetectorGround : MonoBehaviour
{

    public bool isGrounded = false;
    [SerializeField] AudioManager audioManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!isGrounded && collision.gameObject.tag == "Rock" || collision.gameObject.tag == "Ground")
        {
            audioManager.playRockImpact();
            //Debug.Log("----collision against: " + collision.gameObject.tag);
        }

        if (collision.gameObject.layer == 3) {
            isGrounded = true;
        }
        
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrounded = false;
        }
    }

}
