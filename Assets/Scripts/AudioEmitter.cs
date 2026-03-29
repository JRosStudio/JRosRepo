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
            StartCoroutine(WaitForInstance());

        if (AudioManager.audioManager != null)
            AudioManager.audioManager.RegisterEmitter(this);
    }

    private IEnumerator WaitForInstance()
    {
        // Espera hasta que FMOD haya creado la instancia
        yield return new WaitUntil(() => emitter.EventInstance.isValid());
        eventInstance = emitter.EventInstance;
    }

    private void Awake()
    {
        emitter = GetComponent<StudioEventEmitter>();

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
        // Intentar obtener la instancia si aún no es válida
        if (!eventInstance.isValid() && emitter != null)
            eventInstance = emitter.EventInstance;

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
