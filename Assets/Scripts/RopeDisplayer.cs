using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDisplayer : MonoBehaviour
{
    public float rayDistance = 50f;
    [SerializeField]
    GameObject ropeStartPrefab;

    GameObject ropeStart;
    // Update is called once per frame

    private void Start()
    {
       ropeStart = Instantiate(ropeStartPrefab, transform.position, Quaternion.identity);
       //ropeStart.SetActive(false);
    }
    void Update()
    {
        RaycastHit2D rayHit =  Physics2D.Raycast(transform.position, new Vector2(0, 1), rayDistance);
        Debug.DrawRay(transform.position, new Vector2(0, rayDistance), Color.green);
        if (rayHit && Input.GetAxisRaw("Vertical") > 0.8)
        {
            
            ropeStart.transform.position = rayHit.point;
            ropeStart.SetActive(true);


        }
        else {
            ropeStart.SetActive(false);
        }
    }
}
