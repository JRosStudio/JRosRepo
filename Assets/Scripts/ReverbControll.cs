using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbControll : MonoBehaviour
{
    // Instancia del snapshot
    FMOD.Studio.EventInstance snapshotReverbOn;

    // Start is called before the first frame update
    void Start()
    {
        // Cargar el snapshot de reverb
        snapshotReverbOn = FMODUnity.RuntimeManager.CreateInstance("snapshot:/REVERB_ON");
    }

    public void ActivateReverb()
    {
        // Activar el snapshot
        snapshotReverbOn.start();
    }

    public void DeactivateReverb()
    {
        // Desactivar el snapshot (fade out o inmediato)
        snapshotReverbOn.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);  // Usa IMMEDIATE si quieres que sea instantáneo
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("REVERB ON");
            ActivateReverb();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("REVERB OFF");
            DeactivateReverb();
        }
    }

    private void OnDestroy()
    {
        // Asegúrate de liberar la instancia para evitar fugas de memoria
        snapshotReverbOn.release();
    }
}