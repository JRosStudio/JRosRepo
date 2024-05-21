using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{

    public List<GoalItem> listaIngredientes = new List<GoalItem>();



    public void addIngrediente(GoalItem gi) {
        if (listaIngredientes.Count < 3) {
            listaIngredientes.Add(gi);
        }
    }

    public int getIngredientCount() {

        return listaIngredientes.Count;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player")) {




        }
    }

}
