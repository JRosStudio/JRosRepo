using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{

    public List<GoalItem> listaIngredientes = new List<GoalItem>();
    private int paellaCarne;
    private int paellaMarisco;
    private int paellaVerdura;


    public void addIngrediente(string ID, int type,int subtype) {
        if (listaIngredientes.Count < 3) {
            GoalItem gi = new GoalItem();
            gi.setGoalItem(ID,type,subtype);
            listaIngredientes.Add(gi);
            
        }
    }

    public int getIngredientCount() {

        return listaIngredientes.Count;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player")) {

            foreach (GoalItem g in listaIngredientes) {
                Debug.Log( "ITEM: " + g.getID() + " - Type: " + g.getType() + "- Subtype: " + g.getSubtype());

                if (g.getType() == 1) {
                    if (g.getSubtype() == 1) {
                        paellaCarne++;
                    }
                    if (g.getSubtype() == 2)
                    {
                        paellaMarisco++;
                    }
                    if (g.getSubtype() == 3)
                    {
                        paellaVerdura++;
                    }
                }

            }

            Debug.Log(listaIngredientes.Count);

            if (listaIngredientes.Count == 3) { 
                if (paellaCarne == 3)
                {
                    Debug.Log("PAELLA DE CARNE");
                }
                if (paellaMarisco == 3)
                {
                    Debug.Log("PAELLA DE MARISCO");
                }
                if (paellaVerdura == 3)
                {
                    Debug.Log("PAELLA DE VERDURA");
                }
                if(paellaCarne < 3 && paellaMarisco < 3 && paellaVerdura < 3) {
                    Debug.Log("ARROZ CON COSAS");
                }
            }
        }
    }

}
