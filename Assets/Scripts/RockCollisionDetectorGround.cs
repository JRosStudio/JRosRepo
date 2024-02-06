using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollisionDetectorGround : MonoBehaviour
{
    
    public bool isGrounded = false;

        FMOD.Studio.EventInstance rockImpact;

    private void Start()
    {
        rockImpact = FMODUnity.RuntimeManager.CreateInstance("event:/RockImpact");
        

    }

    private void Update()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(rockImpact, gameObject.transform, gameObject.GetComponent<Rigidbody2D>());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!isGrounded && collision.gameObject.tag == "Rock" || collision.gameObject.tag == "Ground")
        {
            rockImpact.start();
            //rockImpact.release();
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
