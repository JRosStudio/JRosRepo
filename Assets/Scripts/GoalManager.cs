using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{

    public bool [] secretObjective;
    public bool [] objectives;
    public string medal;
    public string lvlID;
    [SerializeField] public GameManager gameManager;
    [SerializeField] public LvL_Info lvlInfo;
    private float goldTimeGoal;
    private float silverTimeGoal;
    private float cooperTimeGoal;
    public Timer timer;
    public float finishTime;

    private bool countDone = false;


    private void Start()
    {
        objectives = new bool[lvlInfo.totalObjectives];
        secretObjective = new bool[lvlInfo.totalSecretObjectives];

        for (int i = 0; i < objectives.Length; i++){
            objectives[i] = false;
        }
        
        for (int i = 0; i < secretObjective.Length; i++){
            secretObjective[i] = false;
        }

        goldTimeGoal = lvlInfo.goldTime;
        lvlID = lvlInfo.levelId;
        silverTimeGoal = lvlInfo.silverTime;
        cooperTimeGoal = lvlInfo.bronzeTime;
    }

    public void CollectObjetive( string ID, bool isSecret, int arrayPositon) {
        if (arrayPositon <= objectives.Length && arrayPositon >= 1)
        {
            if (!isSecret)
            {
                GoalItem goal = new GoalItem();
                goal.setGoalItem(ID, isSecret, arrayPositon);
                objectives[arrayPositon - 1] = true;
            }
            else
            {
                GoalItem sGoal = new GoalItem();
                sGoal.setGoalItem(ID, isSecret, arrayPositon);
                secretObjective[arrayPositon - 1] = true;
            }
        }
        else { 
            Debug.Log("El arrayPositon del objetivo está fuera de rango" + "arrayPositon: " + arrayPositon + "objectives.Length: " + objectives.Length);
        }

    }

    public bool isObjectivesArrayEmpty() {
        bool empty = true;
        for (int i = 0; i< objectives.Length; i++) {
            if (objectives[i] != false) {
                empty = false;
            }
        }
        return empty;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && !countDone && !isObjectivesArrayEmpty()) {

            
            finishTime = timer.GetCurrentTime();
            if (finishTime <= goldTimeGoal) {
                Debug.Log("ORO!" + finishTime);
                medal = "gold";
            } else if (finishTime <= silverTimeGoal && finishTime >= goldTimeGoal) {
                Debug.Log("PLATA!: " + finishTime);
                medal = "silver";
            } else if (finishTime >= cooperTimeGoal) {
                Debug.Log("BRONCE!: " + finishTime);
                medal = "bronze";
            }

            gameManager.SaveGame(lvlID, objectives, secretObjective, finishTime, medal);
        }

       /* if (collision.transform.CompareTag("Player") && !countDone && listaIngredientes.Count == 3) {

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
        }*/


        }
    }


