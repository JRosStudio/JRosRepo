using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField]
    public TMP_Text ropesUsed_Txt;

    public Vector3 hitPosition;

    public int ropeLength;
    public int lastRopeLength;

    GameObject ropeStart;
    private ResourceManager resourceManager;

    private bool m_isAxisInUse = false;

    public LayerMask layerMask;

    bool insideWall;

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
        insideWall = player.isInsideWall;
        
        RaycastHit2D rayHit = Physics2D.Raycast(new Vector2 (player.projectileThrowMarker.transform.position.x , transform.position.y), new Vector2(0, 1), rayDistance, ~layerMask );
        Debug.DrawRay(transform.position, new Vector2(0, rayDistance), Color.green);

        if(player.gamePaused == false) { 
            if (rayHit && (Input.GetAxisRaw("RopeState") > 0.8 || Input.GetButton("RopeStateKeyBoard")) && player.ropesHashSet.Count == 0 && rayHit.transform.tag != "Rock" && resourceManager.getCurrentRopes() > 0 && !insideWall)
            {
            
                ropeStart.transform.position = rayHit.point;
                hitPosition = rayHit.point;
                ropeStart.SetActive(true);
                player.readyToShootArrow = true;
            }
            else
            {
                player.readyToShootArrow = false;
                ropeStart.SetActive(false);

                ropesUsed_Txt.enabled = false;
            }
            if ((Input.GetAxisRaw("RopeState") > 0.8 || Input.GetButton("RopeStateKeyBoard")))
            {
                RopeBodyLenght();
                ropesUsed_Txt.enabled = true;

            }

            //reset
            if (lastRopeLength != ropeLength) {
                foreach (var r in ropeBody)
                {
                    Destroy(r);
                }
                ropeBody.Clear();

                for (int i = 1; i <= ropeLength; i++) {
                    GameObject go = Instantiate(ropeBodyPrefab, new Vector3(ropeStart.transform.position.x, ropeStart.transform.position.y - ropeBody.Count - 1, ropeStart.transform.position.z), Quaternion.identity, ropeStart.transform); ;
                    ropeBody.Add(go);
                }

                lastRopeLength = ropeLength;
            }


            ropesUsed_Txt.SetText("(" + (ropeLength + 1) + ")");

            }
    }


    private void RopeBodyLenght() {

       // Debug.Log(Input.GetAxisRaw("RopeAddRemove"));

        if (ropeLength >= resourceManager.getCurrentRopes()) {
            ropeLength = resourceManager.getCurrentRopes() - 1;
        }

        if (ropeLength < 0) {
            ropeLength = 0;
        }

        if (Input.GetKeyDown("up") && ropeLength > 0)
        {
            ropeLength--;
        }

        if (Input.GetKeyDown("down") && ropeLength + 2 <= resourceManager.getCurrentRopes())
        {
            ropeLength++;
        }


        if (Input.GetAxisRaw("RopeAddRemove") >= 0.8 && ropeBody.Count > 0)
        {
            if (m_isAxisInUse == false)
            {
                ropeLength--;
                m_isAxisInUse = true;
            }
        }

        if (Input.GetAxisRaw("RopeAddRemove") <= -0.8 && ropeBody.Count + 2 <= resourceManager.getCurrentRopes())
        {
            if (m_isAxisInUse == false)
            {
                ropeLength++;
                m_isAxisInUse = true;
            }
        }
        if (Input.GetAxisRaw("RopeAddRemove") == 0)
        {
            m_isAxisInUse = false;
        }

      
     
    }

    public void RopeCutter(GameObject obj) {

       ropeLength = ropeBody.IndexOf(obj);

    }

}
