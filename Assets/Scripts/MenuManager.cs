using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [SerializeField]
    Animator animation;

    [SerializeField]
    GameObject BlackScreen;

    Animator animationBlackScreen;

    [SerializeField]
    Player player;

    [SerializeField]
    GameObject firstButton;
    [SerializeField]
    GameObject secondButton;
    [SerializeField]
    GameObject thirdButton;

    [SerializeField]
    GameObject OptionPanel;
    [SerializeField]
    GameObject TutorialPanel;
    [SerializeField]
    GameObject ControlsPanel;

    Button options;
    Button tutorial;
    Button controls;

    public bool optionPanelState;
    public bool tutorialPanelState;
    public bool controlPanelState;


    private void Start()
    {
        animationBlackScreen = BlackScreen.GetComponent<Animator>();
        options = firstButton.GetComponent<Button>();
        tutorial = secondButton.GetComponent<Button>();
        controls = thirdButton.GetComponent<Button>();
    }

    public void pauseMenuHidden()
    {

        animation.SetInteger("PauseMenuState", 0);
        Time.timeScale = 1;
        animationBlackScreen.SetInteger("Transition", 3);
        player.gamePaused = false;
       
    }
    public void pauseMenuOut() {

        options.Select();
        options.interactable = true;
        tutorial.interactable = true;
        controls.interactable = true;
        animation.SetInteger("PauseMenuState", 1);
    }

    public void freezeGame() {

        Time.timeScale = 0;
        animationBlackScreen.SetInteger("Transition", 4);
        player.gamePaused = true;
    }
    public void pauseMenuStay() {

        animation.SetInteger("PauseMenuState", 2);

    }
    public void pauseMenuBack()
    {

        CloseAllPanels();
        options.interactable = false;
        tutorial.interactable = false;
        controls.interactable = false;
        animation.SetInteger("PauseMenuState", 3);
    }

    public void ToggleOptionPanel (){
        if (!optionPanelState)
        {
            optionPanelState = true;
            tutorialPanelState = false;
            controlPanelState = false;

            OptionPanel.SetActive(true);
            TutorialPanel.SetActive(false);
            ControlsPanel.SetActive(false);
        }
        else {
            optionPanelState = false;
            tutorialPanelState = false;
            controlPanelState = false;

            OptionPanel.SetActive(false);
            TutorialPanel.SetActive(false);
            ControlsPanel.SetActive(false);
        }
    }

    public void ToggleTutorialPanel()
    {
        if (!tutorialPanelState)
        {
            optionPanelState = false;
            tutorialPanelState = true;
            controlPanelState = false;

            OptionPanel.SetActive(false);
            TutorialPanel.SetActive(true);
            ControlsPanel.SetActive(false);
        }
        else
        {
            optionPanelState = false;
            tutorialPanelState = false;
            controlPanelState = false;

            OptionPanel.SetActive(false);
            TutorialPanel.SetActive(false);
            ControlsPanel.SetActive(false);
        }
    }

    public void ToggleControlsPanel()
    {
        if (!controlPanelState)
        {
            optionPanelState = false;
            tutorialPanelState = false;
            controlPanelState = true;

            OptionPanel.SetActive(false);
            TutorialPanel.SetActive(false);
            ControlsPanel.SetActive(true);
        }
        else
        {
            optionPanelState = false;
            tutorialPanelState = false;
            controlPanelState = false;

            OptionPanel.SetActive(false);
            TutorialPanel.SetActive(false);
            ControlsPanel.SetActive(false);
        }
    }

    public void CloseAllPanels()
    {
            optionPanelState = false;
            tutorialPanelState = false;
            controlPanelState = false;

            OptionPanel.SetActive(false);
            TutorialPanel.SetActive(false);
            ControlsPanel.SetActive(false);
    }

    public void TogglePause()
    {
        // Cambia el estado de pausa
        player.gamePaused = !player.gamePaused;

        if (player.gamePaused)
        {
            pauseMenuOut();   // abre el menú
        }
        else
        {
            pauseMenuBack(); // reanuda tiempo y cierra el menú
        }
    }

}
