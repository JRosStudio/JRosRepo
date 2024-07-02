using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void animationIddle() { 
        
    }

    public void ActivateCheckPoint() { 
        
    }

    public void clearCheckpoint() { 
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
    }
}
