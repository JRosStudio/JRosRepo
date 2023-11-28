using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    FMOD.Studio.EventInstance soundInstance;
    FMOD.Studio.EventDescription testDescription;
    public int length;


    private void Start()
    {
        soundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music_Test");
        soundInstance.getDescription( out testDescription);
        testDescription.getLength(out  length);
        Debug.Log(length);
    }

    private void Update()
    {
        
    }




}
