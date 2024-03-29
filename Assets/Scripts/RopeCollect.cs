using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCollect : MonoBehaviour
{

    private GameObject ResourceManager;
    public void Awake()
    {
       ResourceManager = GameObject.Find("ResourceManager");

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            ResourceManager.GetComponent<ResourceManager>().addRopes(5);
            Destroy(gameObject);
        }
    }
}
