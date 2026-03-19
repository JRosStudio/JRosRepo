using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    FMOD.Studio.EventInstance soundInstance;
    FMOD.Studio.EventDescription testDescription;
    public int length;

    [Header("Listener")]
    public Transform listener;

    [Header("Occlusion")]
    public LayerMask occlusionMask;
    public float maxDistance = 30f;
    public int emittersPerFrame = 5;
    public float occlusionLerpSpeed = 5f;

    private int currentIndex = 0;

    public static AudioManager audioManager;
    public List<AudioEmitter> emitters = new List<AudioEmitter>();

    [Header("Debug")]
    public bool showRaycasts = true; // activar/desactivar la visualización
    public Color rayColor = Color.red;


    private void Awake()
    {
        if (audioManager != null && audioManager != this)
        {
            Destroy(gameObject);
            return;
        }
        audioManager = this;
    }

    public void RegisterEmitter(AudioEmitter emitter)
    {
        if (!emitters.Contains(emitter))
            emitters.Add(emitter);
    }

    public void UnregisterEmitter(AudioEmitter emitter)
    {
        emitters.Remove(emitter);
    }




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
        if (listener == null || emitters.Count == 0)
            return;

        int processed = 0;
        while (processed < emittersPerFrame && emitters.Count > 0)
        {
            if (currentIndex >= emitters.Count)
                currentIndex = 0;

            AudioEmitter emitter = emitters[currentIndex];
            if (emitter != null && emitter.isActiveAndEnabled)
            {
                UpdateEmitterOcclusion(emitter);
                emitter.UpdateOcclusion(occlusionLerpSpeed);
                processed++;
            }
            currentIndex++;
        }  
    }

    void UpdateEmitterOcclusion(AudioEmitter emitter)
    {
        Vector3 emitterPos = emitter.transform.position;
        Vector3 listenerPos = listener.position;

        Vector3 dir = listenerPos - emitterPos;
        float distance = dir.magnitude;

        if (distance > maxDistance)
        {
            emitter.targetOcclusion = 0f;
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(emitterPos, dir.normalized, distance, occlusionMask);
        if (hit.collider != null)
        {
            emitter.targetOcclusion = 1f;
            //Debug.Log("Hit layer: " + hit.collider.gameObject.layer);
        }
        else {
            emitter.targetOcclusion = 0f;
        }
        
    }

    private void OnDrawGizmos()
    {
        if (!showRaycasts || listener == null || emitters.Count == 0)
            return;

        // Dibuja un círculo/esfera que representa la distancia máxima
        Gizmos.color = Color.green; // color de la esfera
        Gizmos.DrawWireSphere(listener.position, maxDistance);

        foreach (var emitter in emitters)
        {
            if (emitter == null || !emitter.isActiveAndEnabled)
                continue;

            Vector3 start = emitter.transform.position;
            Vector3 end = listener.position;
            Vector3 dir = end - start;
            float distance = dir.magnitude;

            RaycastHit2D hit = Physics2D.Raycast(start, dir.normalized, distance, occlusionMask);
            if (hit.collider != null)
            {
                Debug.DrawLine(start, hit.point, Color.red);
            }
            else
            {
                Debug.DrawLine(start, listener.position, Color.green);
            }
        }
    }

}
