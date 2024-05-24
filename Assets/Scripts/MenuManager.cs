using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField]
    Animator animation;


    public void pauseMenuHidden()
    {
        animation.SetInteger("PauseMenuState", 0);
    }
    public void pauseMenuOut() {

        animation.SetInteger("PauseMenuState", 1);
    }

    public void pauseMenuStay() {
        animation.SetInteger("PauseMenuState", 2);
    }
    public void pauseMenuBack()
    {
        animation.SetInteger("PauseMenuState", 3);
    }

}
