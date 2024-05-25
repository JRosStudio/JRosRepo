using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField]
    Animator animation;

    [SerializeField]
    Player player;


    public void pauseMenuHidden()
    {
        animation.SetInteger("PauseMenuState", 0);
        Time.timeScale = 1;
        player.gamePaused = false;
    }
    public void pauseMenuOut() {

        animation.SetInteger("PauseMenuState", 1);
    }

    public void freezeGame(){
        Time.timeScale = 0;
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
