using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MainMenu_AudioManager : MonoBehaviour
{
    private EventInstance mainMenuMusicInstance;

    void Start()
    {
        // 1. Cargar banks necesarios
        RuntimeManager.LoadBank("Master", true);
        RuntimeManager.LoadBank("Master.strings", true);
        RuntimeManager.LoadBank("MainMenu", true);

        // 2. Crear instancia del evento
        mainMenuMusicInstance = RuntimeManager.CreateInstance("event:/MainMenuMusic");

        // 3. Iniciar la música
        mainMenuMusicInstance.start();
    }

    void OnDestroy()
    {
        // 4. Parar y liberar el evento correctamente
        mainMenuMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        mainMenuMusicInstance.release();

        // 5. Descargar el bank del menú
        RuntimeManager.UnloadBank("MainMenu");
    }
}
