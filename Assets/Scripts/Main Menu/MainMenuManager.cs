using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject selectLevelPos;
    [SerializeField] GameObject mainMenuPos;
    [SerializeField] GameObject camara;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject selectLevelMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject exitMenu;
    [SerializeField] GameObject actors;
    [SerializeField] GameObject keyboardTuto;
    [SerializeField] GameObject controllerTuto;
    [SerializeField] GameObject textTuto_OBJ;
    [SerializeField] GameObject Button_Start_OBJ;
    private Button Button_Start;
    private TextMeshProUGUI textTuto;
    


    Animator cam_Animator;
    Animator actors_Animator;

    public bool friendsGone;
    public bool animationDone;

    public void Start()
    {

        camara.transform.position = mainMenuPos.transform.position;
        cam_Animator = camara.GetComponent<Animator>();
        actors_Animator = actors.GetComponent<Animator>();
        friendsGone = false;
        textTuto = textTuto_OBJ.GetComponent<TextMeshProUGUI>();
        Button_Start = Button_Start_OBJ.GetComponent<Button>();
        Button_Start.Select();
    }

    public void LoadGame() {
        SceneManager.LoadScene("Lvl1");
    }

    public void openSettings() {

        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void closeSettings()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void closeExit()
    {
        mainMenu.SetActive(true);
        exitMenu.SetActive(false);
    }

    public void openExit()
    {
        mainMenu.SetActive(false);
        exitMenu.SetActive(true);
    }

    public void SettingsChangeImage() {
        if (keyboardTuto.activeSelf) {
            textTuto.text = "Watch keyboard";
            controllerTuto.SetActive(true);
            keyboardTuto.SetActive(false);
        }
        else if (controllerTuto.activeSelf)
        {
            textTuto.text = "Watch controller";
            keyboardTuto.SetActive(true);
            controllerTuto.SetActive(false);
        }
    } 

    public void toSelectLevel() {
        if (friendsGone) {
            //camara.transform.position = selectLevelPos.transform.position;
            mainMenu.SetActive(false);
            selectLevelMenu.SetActive(true);
            cam_Animator.SetTrigger("ToSelectLevel");
            actors_Animator.SetTrigger("InterruptScene");
            friendsGone = true;
            animationDone = true;
        }
        friendsGone = true;
    } 

    public void selectLevel() {
        if (friendsGone == false && animationDone == false)
        {
            //camara.transform.position = selectLevelPos.transform.position;
            mainMenu.SetActive(false);
            selectLevelMenu.SetActive(true);
            cam_Animator.SetTrigger("ToSelectLevel");
            friendsGone = true;
            animationDone = true;
        }
    }
    
    public void AnimationDone() {
        animationDone = true;
    }

    public void ToMainMenu() {
        mainMenu.SetActive(true);
        selectLevelMenu.SetActive(false);
        cam_Animator.SetTrigger("ToMainMenu");
    }

    public void CloseGame() {
        Application.Quit();
    }

}
