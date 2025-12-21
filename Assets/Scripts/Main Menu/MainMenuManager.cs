using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]GameObject selectLevelPos;
    [SerializeField]GameObject mainMenuPos;
    [SerializeField] GameObject camara;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject selectLevelMenu;

    public void Start()
    {
        camara.transform.position = mainMenuPos.transform.position;
    }
    public void LoadGame() {
        SceneManager.LoadScene("Lvl1");
    }


    public void selectLevel() {
        camara.transform.position = selectLevelPos.transform.position;
        mainMenu.SetActive(false);
        selectLevelMenu.SetActive(true);
    }

}
