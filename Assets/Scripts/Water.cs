using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    public GameObject myPrefab;


    [SerializeField]
    private Player player;

    private void Start()
    {
        player.isOnWater(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(myPrefab, new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z), Quaternion.identity);
            player.isOnWater(true);
        }

        if (collision.gameObject.CompareTag("Rock"))
        {
            Instantiate(myPrefab, new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z), Quaternion.identity);
            collision.gameObject.GetComponent<RockMoval>().isInWater = true;


        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /*if (!collision.gameObject.CompareTag("ThrowRock")) {
            Instantiate(myPrefab, new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z), Quaternion.identity);
        }*/
        
        if (collision.gameObject.CompareTag("Player"))
        {
            player.isOnWater(false);
            Instantiate(myPrefab, new Vector3(collision.transform.position.x, collision.transform.position.y + 0.15f, collision.transform.position.z), Quaternion.identity);
        }

        if (collision.gameObject.CompareTag("Rock"))
        {
            collision.gameObject.GetComponent<RockMoval>().isInWater = false;
            
        }

    }

}
