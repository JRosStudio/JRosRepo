using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollisionDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("||| collision against: " + collision.gameObject.tag);
    }
}
