using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

    public class GameManager : MonoBehaviour
{
    [SerializeField] LvL_Info lvl_Info;

    public List<LvL_Score> lvlGameData;
    public LvL_GameDataWrapper gameData;

    public TMP_Text TXT_goldTime;
    public TMP_Text TXT_playerTime;

    private int oldRaceObjetsColected;
    private int oldRaceSecretObjetsColected;
    private int newRaceObjetsColected;
    private int newRaceSecretObjetsColected;
    //JSON
    private string filePath;


    public void Start()
    {
        filePath = Application.persistentDataPath + "/savefile.json";
        lvlGameData = new List<LvL_Score>();
        gameData = LoadScore();
        if (gameData == null)
        {
            Debug.Log("GAME DATA NULL");
            gameData = new LvL_GameDataWrapper(lvlGameData);
        }
        else {
            Debug.Log("LISTA DE RESULTADOS CARGADA");
            lvlGameData = gameData.lvlGameData;
        }
    }


    // SAVE GAME
    public void SaveGame(string lvlID ,bool[] localObjectivesCollectedArray, bool [] localSecretObjectivesCollecte, float localPlayerTime, string medal)
    {
        LvL_Score lvlScore = new LvL_Score();
        lvlScore.objectivesCollected = localObjectivesCollectedArray;
        lvlScore.secretObjetivesCollected = localSecretObjectivesCollecte;
        lvlScore.playerTime = localPlayerTime;
        lvlScore.medal = medal;
        lvlScore.lvlID = lvlID;
        string json;

        // si la lista de niveles guardados está vacía
        if (lvlGameData.Count == 0)
        {
            Debug.Log("NUEVO- AÑADE PRIMER NIVEL");
            lvlGameData.Add(lvlScore);
        }
        // si la lista tiene ya datos
        else {
            // si la lista de niveles guardados no está vacía la recorre
            bool exsiste = false;
            foreach (LvL_Score lvlS in lvlGameData) {
                //Si ya hay un nivel con datos guardados comprueba si el nuevo resultado es mejor
                if (lvlS.lvlID == lvlScore.lvlID)
                {
                    Debug.Log("EXISTEN DATOS DE NIVEL - COMPROBANDO");
                    exsiste = true;
                    oldRaceObjetsColected = 0;
                    oldRaceSecretObjetsColected = 0;
                    newRaceObjetsColected = 0;
                    newRaceSecretObjetsColected = 0;

                    foreach (bool valor in lvlS.objectivesCollected)
                    {
                        if (valor)
                        {
                            oldRaceObjetsColected++;
                        }
                    }
                    foreach (bool sValor in lvlS.secretObjetivesCollected)
                    {
                        if (sValor)
                        {
                            oldRaceSecretObjetsColected++;
                        }
                    }
                    foreach (bool valor in lvlScore.objectivesCollected)
                    {
                        if (valor)
                        {
                            newRaceObjetsColected++;
                        }
                    }
                    foreach (bool sValor in lvlScore.secretObjetivesCollected)
                    {
                        if (sValor)
                        {
                            newRaceSecretObjetsColected++;
                        }
                    }
                    if (newRaceObjetsColected > oldRaceObjetsColected)
                    {
                        //El jugador ha recogido más objetivos, así que actualiza los datos guardados con los datos de esta carrera
                        Debug.Log("SE HAN RECOGIDO MÁS OBJETIVOS - ACTUALIZADO");
                        lvlS.objectivesCollected = lvlScore.objectivesCollected;
                        lvlS.secretObjetivesCollected = lvlScore.secretObjetivesCollected;
                        lvlS.playerTime = lvlScore.playerTime;
                        lvlS.medal = lvlScore.medal;
                    }
                    else if (newRaceObjetsColected == oldRaceObjetsColected)
                    {
                        if (newRaceSecretObjetsColected > oldRaceSecretObjetsColected)
                        {
                            //El jugador tiene los mismos objetivos recogidos, pero tiene más objetivos secretos, así que actualiza los datos guardados con los datos de esta carrera
                            Debug.Log("SE HAN RECOGIDO MISMOS OBJETIVOS, PERO MÁS OBJETIVOS  SECRETOS - ACTUALIZADO");
                            lvlS.objectivesCollected = lvlScore.objectivesCollected;
                            lvlS.secretObjetivesCollected = lvlScore.secretObjetivesCollected;
                            lvlS.playerTime = lvlScore.playerTime;
                            lvlS.medal = lvlScore.medal;
                        }
                        else if (newRaceSecretObjetsColected == oldRaceSecretObjetsColected)
                        {
                            if ( lvlScore.playerTime < lvlS.playerTime)
                            {
                                //El jugador tiene los mismos objetivos recogidos, y los mismos objetivos secretos, pero ha hecho mejor tiempo, así que actualiza los datos guardados con los datos de esta carrera
                                Debug.Log("SE HAN RECOGIDO MISMOS OBJETIVOS Y MISMOS OBJETIVOS  SECRETOS, PERO MEJOR TIEMPO - ACTUALIZADO");
                                lvlS.objectivesCollected = lvlScore.objectivesCollected;
                                lvlS.secretObjetivesCollected = lvlScore.secretObjetivesCollected;
                                lvlS.playerTime = lvlScore.playerTime;
                                lvlS.medal = lvlScore.medal;
                            }
                        }
                    }
                }
            }
            //Si no se han encontrado coincidencias añade la score a la lista
            if (!exsiste) {
                Debug.Log("NO ES EL PRIMER NIVEL Y ADEMÁS, ES NUEVO - AÑADIDO");
                lvlGameData.Add(lvlScore);
            }
        }
        // Guarda la lista de scores
         gameData = new LvL_GameDataWrapper(lvlGameData);

        json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Save Game In: " + filePath);
        Debug.Log("Data:\n" + json);
    }

    public LvL_GameDataWrapper LoadScore()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            LvL_GameDataWrapper score = JsonUtility.FromJson<LvL_GameDataWrapper>(json);
            Debug.Log("Game Loaded");
            return score;
        }
        else {
            Debug.Log("Couldn't find Save Files");
            return null;
        }
    }

    //LIFES

    //LOAD GAME

    // Esquema datos nivel



}
