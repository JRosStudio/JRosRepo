using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    FMOD.Studio.EventInstance soundInstance;
    FMOD.Studio.EventDescription testDescription;
    public int length;

    //FMOD.Studio.EventInstance rockImpact;

    private void Start()
    {
       
        soundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music_Test");
        //rockImpact = FMODUnity.RuntimeManager.CreateInstance("event:/RockImpact");

        soundInstance.getDescription( out testDescription);
        testDescription.getLength(out  length);
        //Debug.Log(length);
    }

    public void playRockImpact() {
        //rockImpact.start();
    }

    public void PlayStep() {
        FMOD.Studio.EventInstance stepInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Steps");
        stepInstance.start();
        stepInstance.release(); // Liberar la instancia después de iniciarla
    }

    public void PlayJump()
    {
        FMOD.Studio.EventInstance jumpInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Jump");
        jumpInstance.start();
        jumpInstance.release(); // Liberar la instancia después de iniciarla
    }

    public void PlayBell() {
        FMOD.Studio.EventInstance bellInstance = FMODUnity.RuntimeManager.CreateInstance("event:/DeathThreshHold");
        bellInstance.start();
        bellInstance.release(); // Liberar la instancia después de iniciarla
    }



    private void Update()
    {
        
    }




}
