using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalItem : MonoBehaviour
{
    public  string ID;
    public  bool isSecret;
    public  int arrayPositon;

    public void setGoalItem(string id, bool secret, int arrayPos) {
        ID = id;
        secret = isSecret;
        arrayPos = arrayPositon;
    }

    public string getID()
    {
        return ID;

    }

    public bool GetIsSecret()
    {
        return isSecret;

    }

    public int GetArrayPosition()
    {
        return arrayPositon;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GoalManager goalManager = GameObject.Find("GoalManager").GetComponent<GoalManager>();
        if (collision.transform.CompareTag("Player")) {
                goalManager.CollectObjetive(ID, isSecret, arrayPositon);
                Destroy(gameObject);
        }   
    }

}
