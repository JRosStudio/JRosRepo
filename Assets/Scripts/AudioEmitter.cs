using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioEmitter : MonoBehaviour
{

    public EventInstance eventInstance;
    private StudioEventEmitter emitter;

    [Header("Oclusión")]
    [Range(0f, 1f)]
    public float occlusion = 0f;          // valor actual suavizado
    [HideInInspector]
    public float targetOcclusion = 0f;    // valor objetivo calculado por raycast


    private void Start()
    {
        if (emitter != null)
        {
            // Aquí la instancia ya debería existir porque el evento se reproduce en Start
            eventInstance = emitter.EventInstance;
        }

        if (AudioManager.audioManager != null)
            AudioManager.audioManager.RegisterEmitter(this);
    }

    private void Awake()
    {
        emitter = GetComponent<StudioEventEmitter>();
        if (emitter != null)
        {
            // Consigue la instancia interna del emitter
            eventInstance = emitter.EventInstance;
        }
    }

    /*private void Update()
    {
        eventInstance.setParameterByName("Occlusion", occlusion);
    }*/

    public void SetOcclusion(float value)
    {
        targetOcclusion = Mathf.Clamp01(value);

        if (eventInstance.isValid())
        {
            eventInstance.setParameterByName("Occlusion", occlusion);
        }
    }

    // Se llama desde el AudioManager cada frame para actualizar suavemente
    public void UpdateOcclusion(float lerpSpeed)
    {
        if (!eventInstance.isValid())
            return;

        // Suavizado
        occlusion = Mathf.Lerp(occlusion, targetOcclusion, Time.deltaTime * lerpSpeed);
        eventInstance.setParameterByName("Occlusion", occlusion);
    }

    private void OnEnable()
    {
        if (AudioManager.audioManager != null)
            AudioManager.audioManager.RegisterEmitter(this);
    }

    private void OnDisable()
    {
        if (AudioManager.audioManager != null)
            AudioManager.audioManager.UnregisterEmitter(this);
    }
}
