using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSmoke : MonoBehaviour
{
    [SerializeField]
    GameObject smokeJump;
    public void SpawnSmokeJump()
    {
        GameObject smk = Instantiate(smokeJump, gameObject.transform.position, Quaternion.identity);
        Vector3 smkLocalScale = smk.transform.localScale;
        if (gameObject.transform.localScale.x > 0)
        {
            smkLocalScale.x = smkLocalScale.x * -1;
            smk.transform.localScale = smkLocalScale;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.tag == "Ground" || collision.gameObject.layer == 3)
        {
            SpawnSmokeJump();
        }*/
    }


}
