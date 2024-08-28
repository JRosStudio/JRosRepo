using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    private GameObject staminaManager;
    public void Awake()
    {
       staminaManager = GameObject.Find("StaminaManager");

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && staminaManager.GetComponent<StaminaManagement>().GetCurrentFood() < 3 ) {
            staminaManager.GetComponent<StaminaManagement>().CollectFood(); ;
            gameObject.SetActive(false);
        }
    }
}
