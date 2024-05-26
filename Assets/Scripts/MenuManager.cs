using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField]
    Animator animation;

    [SerializeField]
    GameObject BlackScreen;

    Animator animationBlackScreen;

    [SerializeField]
    Player player;

    private void Start()
    {
        animationBlackScreen = BlackScreen.GetComponent<Animator>();
    }

    public void pauseMenuHidden()
    {
        animation.SetInteger("PauseMenuState", 0);
        Time.timeScale = 1;
        animationBlackScreen.SetInteger("Transition", 3);
        player.gamePaused = false;
    }
    public void pauseMenuOut() {

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
        animation.SetInteger("PauseMenuState", 3);
    }
           
}
