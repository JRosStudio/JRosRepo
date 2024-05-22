using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{

    public List<GoalItem> listaIngredientes = new List<GoalItem>();
    private bool paellaCarne = false;
    private bool paellaMarisco = false;
    private bool paellaVerdura = false;


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
                    if (g.getSubtype() == 1)
                    {
                        paellaCarne = true;
                    }    
                
                    if (g.getSubtype() == 2)
                    {

                        paellaMarisco = true;
                    }

                    }
                    if (g.getSubtype() == 3)
                    {
                        paellaVerdura = true;
  
                    }
                }

            }

            if (listaIngredientes.Count == 3) { 
                if (paellaCarne && !paellaMarisco && !paellaVerdura)
                {
                    Debug.Log("PAELLA DE CARNE");
                }
                else if (!paellaCarne && paellaMarisco && !paellaVerdura)
                {
                    Debug.Log("PAELLA DE MARISCO");
                }
                else if (!paellaCarne && !paellaMarisco && paellaVerdura)
                {
                    Debug.Log("PAELLA DE VERDURA");
                }
                else {
                    Debug.Log("ARROZ CON COSAS");
                }
            }
        }
    }


