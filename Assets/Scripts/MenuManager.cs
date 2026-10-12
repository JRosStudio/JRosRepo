using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    GameObject fourthButton;

    [SerializeField]
    GameObject OptionPanel;
    [SerializeField]
    GameObject TutorialPanel;
    [SerializeField]
    GameObject ControlsPanel;


    [SerializeField]
    GameObject keyboardIMG;

    [SerializeField]
    GameObject controllerIMG;

    Button options;
    Button tutorial;
    Button controls;
    Button exit;

    public bool optionPanelState;
    public bool tutorialPanelState;
    public bool controlPanelState;

    Resolution[] resolutions;
    Resolution startingResolution;
    List<Resolution> resolutions169;
    public TMPro.TMP_Dropdown resolutionDropdown;

    private void Start()
    {
        animationBlackScreen = BlackScreen.GetComponent<Animator>();
        options = firstButton.GetComponent<Button>();
        tutorial = secondButton.GetComponent<Button>();
        controls = thirdButton.GetComponent<Button>();
        exit = fourthButton.GetComponent<Button>();

        //Resolution dropdown
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> resolutionsStringList = new List<string>();
        resolutions169 = new List<Resolution>();
        const float targetAspect = 16f / 9f;
        const float tolerance = 0.01f;

        for (int i = 0; i < resolutions.Length; i++)
        {
            float aspect = (float)resolutions[i].width / resolutions[i].height;

            if (Mathf.Abs(aspect - targetAspect) < tolerance)
            {

                resolutions169.Add(resolutions[i]);
                string res = resolutions[i].width + " x " + resolutions[i].height;
                resolutionsStringList.Add(res);
                startingResolution = resolutions[i];
            }
        }
        resolutionDropdown.AddOptions(resolutionsStringList);
        resolutionDropdown.SetValueWithoutNotify(resolutions169.Count - 1);
        Screen.SetResolution(startingResolution.width, startingResolution.height, Screen.fullScreen);

    }

    public void pauseMenuHidden()
    {

        animation.SetInteger("PauseMenuState", 0);
        Time.timeScale = 1;
        animationBlackScreen.SetInteger("Transition", 3);
        player.gamePaused = false;

    }
    public void pauseMenuOut()
    {

        options.Select();
        options.interactable = true;
        tutorial.interactable = true;
        controls.interactable = true;
        exit.interactable = true;
        animation.SetInteger("PauseMenuState", 1);
    }

    public void freezeGame()
    {
        Time.timeScale = 0;
        animationBlackScreen.SetInteger("Transition", 4);
        player.gamePaused = true;
    }
    public void pauseMenuStay()
    {
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

    public void ToggleOptionPanel()
    {
        if (!optionPanelState)
        {
            optionPanelState = true;
            tutorialPanelState = false;
            controlPanelState = false;

            OptionPanel.SetActive(true);
            TutorialPanel.SetActive(false);
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

    public void ControllsNext()
    {

        bool nextState = !keyboardIMG.activeSelf;

        keyboardIMG.SetActive(nextState);
        controllerIMG.SetActive(!nextState);

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions169[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
