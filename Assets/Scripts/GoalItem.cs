using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalItem : MonoBehaviour
{
    public  string ID;
    public  int Type;
    public  int Subtype;

    /*ID:
     Name of ITEM
    */


    /*types:
    1- Food
    2- Person
    */

    /*subtypes:
    1- Paella Carne | Familia
    2- Paella pescado | Amigo
    3- Paella verduras | Compañero de trabajo
    */

    public void setGoalItem(string id, int type, int subtype) {
        ID = id;
        Type = type;
        Subtype = subtype;
    }

    public string getID()
    {
        return ID;

    }

    public int getType()
    {
        return Type;

    }

    public int getSubtype()
    {
        return Subtype;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GoalManager goalManager = GameObject.Find("GoalManager").GetComponent<GoalManager>();
        if (collision.transform.CompareTag("Player")) {
            if (goalManager.getIngredientCount() < 3 && Type == 1) {
                goalManager.addIngrediente(ID, Type, Subtype);
                Destroy(gameObject);
            }
            if (goalManager.getPersonasCount() < 3 && Type == 2)
            {
                goalManager.addPersona(ID, Type, Subtype);
                Destroy(gameObject);
            }
        }   
    }

}
