using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalItem : MonoBehaviour
{
    public string ID;
    public int type;
    public int subtype;

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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GoalManager goalManager = GameObject.Find("GoalManager").GetComponent<GoalManager>();
        if (collision.transform.CompareTag("Player") && goalManager.getIngredientCount() < 3) {  
            Debug.Log(this.ID);
            //goalManager.addIngrediente(new GoalItem(ID,type,sbyte));
            Destroy(gameObject);
        }   
    }

}