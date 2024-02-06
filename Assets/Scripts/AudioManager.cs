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

    private void Update()
    {
        
    }




}
