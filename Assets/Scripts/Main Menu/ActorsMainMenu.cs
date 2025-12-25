using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorsMainMenu : MonoBehaviour
{

    [SerializeField] Animator animation;
    [SerializeField] SpriteRenderer renderer;
    bool risaState = false;

    public bool flipable;

    // Update is called once per frame
    void Update()
    {
        risaState = animation.GetBool("Risa");
        if (risaState == false) {
           int disparador = Random.Range(0, 20000);
            if (disparador >= 19950) {
                animation.SetBool("Risa", true);
            }
            if (flipable == true && disparador >= 19950) {
                renderer.flipX = !renderer.flipX;
            }
            
        }
        //Debug.Log("RISA_STATE = " + risaState);
    }
    public void setRisaFalse() {
        animation.SetBool("Risa", false);
    }
}
