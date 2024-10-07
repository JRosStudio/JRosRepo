using System.Collections;
using UnityEngine;
using FMODUnity;  // Asegúrate de que FMOD esté configurado en tu proyecto

public class Enemigo : MonoBehaviour
{
    [SerializeField]
    private int health;

    private bool isShaking = false;

    [FMODUnity.EventRef]
    public string fmodEventPath = "event:/Mine_Rock";  // Path del evento en FMOD

    public void takeDamage(int damage)
    {
        health -= damage;

        // Llamar al sonido con FMOD
        PlayDamageSound();

        if (health > 0 && !isShaking)
        {
            // Si la salud es mayor a 0, iniciar el "shake"
            StartCoroutine(Shake(0.1f, 0.05f));  // Duración y magnitud del shake
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void PlayDamageSound()
    {
        // Crear instancia del evento
        FMOD.Studio.EventInstance soundEvent = RuntimeManager.CreateInstance(fmodEventPath);

        // Establecer la variable Rock_State según la salud del enemigo
        int rockState = (health > 0) ? 1 : 0;
        soundEvent.setParameterByName("Rock_State", rockState);

        // Reproducir el sonido
        soundEvent.start();

        // Liberar la instancia después de reproducirla
        soundEvent.release();
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        isShaking = true;

        Vector3 originalPosition = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float xOffset = Random.Range(-1f, 1f) * magnitude;
            float yOffset = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPosition.x + xOffset, originalPosition.y + yOffset, originalPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;  // Esperar hasta el siguiente frame
        }

        transform.localPosition = originalPosition;
        isShaking = false;
    }
}