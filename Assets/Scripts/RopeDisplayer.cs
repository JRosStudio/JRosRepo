using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDisplayer : MonoBehaviour
{
    public float rayDistance = 50f;
    [SerializeField]
    public GameObject ropeStartPrefab;
    [SerializeField]
    public GameObject ropeBodyPrefab;
    [SerializeField]
    Player player;
    public List<GameObject> ropeBody;

    public Vector3 hitPosition;

    public int ropeLength;

    GameObject ropeStart;
    private ResourceManager resourceManager;


    public void Awake()
    {
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
    }

    private void Start()
    {
        ropeStart = Instantiate(ropeStartPrefab, transform.position, Quaternion.identity);
        //ropeStart.SetActive(false);
    }
    void Update()
    {
        
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, new Vector2(0, 1), rayDistance);
        Debug.DrawRay(transform.position, new Vector2(0, rayDistance), Color.green);
        if (rayHit && Input.GetAxisRaw("RopeState") > 0.8 && player.ropesHashSet.Count == 0 && rayHit.transform.tag != "Rock" && resourceManager.getCurrentRopes() > 0)
        {
            RopeBodyLenght();
            ropeStart.transform.position = rayHit.point;
            hitPosition = rayHit.point;
            ropeStart.SetActive(true);
            gameObject.GetComponentInParent<Player>().readyToShootArrow = true;
        }
        else
        {
            gameObject.GetComponentInParent<Player>().readyToShootArrow = false;
            foreach (var r in ropeBody) {
                Destroy(r);
            }
            ropeBody.Clear();
            ropeLength = ropeBody.Count;
            ropeStart.SetActive(false);
        }
    }

    private void RopeBodyLenght() {

        Debug.Log(ropeBody.Count);
        
        if (Input.GetKeyDown("up") && ropeBody.Count > 0)
        {
            Destroy(ropeBody[ropeBody.Count - 1]);
            ropeBody.RemoveAt(ropeBody.Count - 1);
            ropeLength = ropeBody.Count;
        }

        if (Input.GetKeyDown("down") && ropeBody.Count + 1 <= resourceManager.getCurrentRopes()  )
        {
            GameObject go = Instantiate(ropeBodyPrefab, new Vector3(ropeStart.transform.position.x, ropeStart.transform.position.y - ropeBody.Count - 1, ropeStart.transform.position.z), Quaternion.identity, ropeStart.transform); ;
            ropeBody.Add(go);
            ropeLength = ropeBody.Count;
}
    }


}
