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

    Button options;
    Button tutorial;
    Button controls;

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

    public void freezeGame(){
        Time.timeScale = 0;
        animationBlackScreen.SetInteger("Transition", 4);
        player.gamePaused = true;
    }
    public void pauseMenuStay() {
        animation.SetInteger("PauseMenuState", 2);
 
    }
    public void pauseMenuBack()
    {

        options.interactable = false;
        tutorial.interactable = false;
        controls.interactable = false;
        animation.SetInteger("PauseMenuState", 3);
    }
           
}
