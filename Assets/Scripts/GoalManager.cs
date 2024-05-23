using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{

    public List<GoalItem> listaIngredientes = new List<GoalItem>();
    public List<GoalItem> listaPersonas = new List<GoalItem>();
    private bool paellaCarne = false;
    private bool paellaMarisco = false;
    private bool paellaVerdura = false;
    private bool countDone = false;

    private int familia;
    private int amigos;
    private int compasTrabajo;



    public void addIngrediente(string ID, int type,int subtype) {
        if (listaIngredientes.Count < 3) {
            GoalItem gi = new GoalItem();
            gi.setGoalItem(ID,type,subtype);
            listaIngredientes.Add(gi);
            
        }
    }  
    public void addPersona(string ID, int type,int subtype) {
        if (listaPersonas.Count < 3) {
            GoalItem gi = new GoalItem();
            gi.setGoalItem(ID,type,subtype);
            listaPersonas.Add(gi);
        }
    }

    public int getIngredientCount() {

        return listaIngredientes.Count;
    }
    public int getPersonasCount()
    {

        return listaPersonas.Count;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && !countDone && listaIngredientes.Count == 3) {

            foreach (GoalItem g in listaIngredientes) {
                //Debug.Log( "ITEM: " + g.getID() + " - Type: " + g.getType() + "- Subtype: " + g.getSubtype());

                if (g.getType() == 1) {
                    if (g.getSubtype() == 1)
                    {
                        paellaCarne = true;
                    }    
                
                    if (g.getSubtype() == 2)
                    {

                        paellaMarisco = true;
                    }
                    if (g.getSubtype() == 3)
                    {
                        paellaVerdura = true;

                    }
                }
              
            }

            foreach (GoalItem g in listaPersonas)
            {
                Debug.Log("PERSONA: " + g.getID() + " - Type: " + g.getType() + "- Subtype: " + g.getSubtype());

                if (g.getType() == 2)
                {
                    if (g.getSubtype() == 1)
                    {
                        familia++;
                    }
                    if (g.getSubtype() == 2)
                    {

                        amigos++;
                    }
                    if (g.getSubtype() == 3)
                    {
                        compasTrabajo++;
                    }
                }
            }
            if (listaIngredientes.Count == 3)
            {
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
                else
                {
                    Debug.Log("ARROZ CON COSAS");
                }

                if (listaPersonas.Count == 0) {
                    Debug.Log("PRÁCTICA EN SOLITARIO");
                }
                else if (listaPersonas.Count == 1)
                {
                    Debug.Log("PAELLA ÍNTIMA");
                    if (familia ==1 && amigos == 0 && compasTrabajo == 0) {
                        Debug.Log("FAMILIA Y AMIGO");
                    }
                    if (familia == 0 && amigos == 1 && compasTrabajo == 0)
                    {
                        Debug.Log("TEA TIME");
                    }
                    if (familia == 0 && amigos == 0 && compasTrabajo == 1)
                    {
                        Debug.Log("AMIGO DEL TRABAJO");
                    }
                }
                else if (listaPersonas.Count == 2)
                {
                    Debug.Log("PAELLA MEDIANA");
                    if (familia == 2 && amigos == 0 && compasTrabajo == 0)
                    {
                        Debug.Log("AUNQUE NO ESTEMOS TODOS, LO IMPORTANTE ES JUNTARSE");
                    }
                    if (familia == 0 && amigos == 2 && compasTrabajo == 0)
                    {
                        Debug.Log("TOCA PONERSE AL DÍA");
                    }
                    if (familia == 0 && amigos == 0 && compasTrabajo == 2)
                    {
                        Debug.Log("LO TERMINAMOS MIENTRAS SE HACE LA COMIDA");
                    }
                    if (familia == 1 && amigos == 1 && compasTrabajo == 0)
                    {
                        Debug.Log("HERMANO DE OTRA MADRE");
                    }
                    if (familia == 1 && amigos == 0 && compasTrabajo == 1)
                    {
                        Debug.Log("NO SÉ SI TENEMOS UN HUECO EN LA EMPRESA PARA...");
                    }
                    if (familia == 0 && amigos == 1 && compasTrabajo == 1)
                    {
                        Debug.Log("¿A TÍ TAMBIÉN TE GUSTAN LOS JUEGOS DE MESA?");
                    }
                }
                else if (listaPersonas.Count == 3)
                {
                    Debug.Log("PAELLA GRANDE");
                    if (familia == 3 && amigos == 0 && compasTrabajo == 0)
                    {
                        Debug.Log("LA PAELLA DE LOS DOMINGO");
                    }
                    if (familia == 0 && amigos == 3 && compasTrabajo == 0)
                    {
                        Debug.Log("PAELLA DE AMIGOS");
                    }
                    if (familia == 0 && amigos == 0 && compasTrabajo == 3)
                    {
                        Debug.Log("MEJORANDO EL AMBIENTE LABORAL");
                    }
                    if (familia == 1 && amigos == 1 && compasTrabajo == 1)
                    {
                        Debug.Log("QUEDADA INUSUAL");
                    }
                    if (familia == 2 && amigos == 1 && compasTrabajo == 0)
                    {
                        Debug.Log("UNO MÁS DE LA FAMILIA");
                    }
                    if (familia == 2 && amigos == 0 && compasTrabajo == 1)
                    {
                        Debug.Log("NO SE CREÍA QUE HACÍAS LA MEJOR PAELLA");
                    }
                    if (familia == 1 && amigos == 2 && compasTrabajo == 0)
                    {
                        Debug.Log("UNO MÁS DEL GRUPO");
                    }
                    if (familia == 0 && amigos == 2 && compasTrabajo == 1)
                    {
                        Debug.Log("PARTIDA DE ROL");
                    }
                    if (familia == 1 && amigos == 0 && compasTrabajo == 2)
                    {
                        Debug.Log("NO SÉ CÓMO HEMOS LLEGADO A ESTO");
                    }
                    if (familia == 0 && amigos == 1 && compasTrabajo == 2)
                    {
                        Debug.Log("ÉL ME ENSEÑÓ TODO LO QUE SÉ");
                    }
                }
                countDone = true;
            }
        }

            
        }
    }


