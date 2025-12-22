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
    Animator cam_Animator;
    public bool friendsGone;
    public bool animationDone;

    public void Start()
    {
        camara.transform.position = mainMenuPos.transform.position;
        cam_Animator = camara.GetComponent<Animator>();
        friendsGone = false;
    }
    public void LoadGame() {
        SceneManager.LoadScene("Lvl1");
    }


    public void toSelectLevel() {
        if (friendsGone) {
            //camara.transform.position = selectLevelPos.transform.position;
            mainMenu.SetActive(false);
            selectLevelMenu.SetActive(true);
            cam_Animator.SetTrigger("ToSelectLevel");
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
    
}
