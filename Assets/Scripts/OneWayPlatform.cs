using UnityEngine;
using UnityEngine.Tilemaps;

public class OneWayPlatform : MonoBehaviour
{
    //private TilemapCollider2D tilemapCollider;
    private BoxCollider2D tilemapCollider;
    private float counter = 0f;

    [SerializeField]
    private Player player;

    private bool canTrigger = true; //  bandera para evitar repetición
    private bool comboHeld = false; //  indica si ↓ + salto siguen pulsados

    public float maxCounter = 0.3f;

    private void Start()
    {
        //tilemapCollider = GetComponent<TilemapCollider2D>();
        tilemapCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {   
        // Detectamos si la combinación está activa actualmente
        comboHeld = player.vertical < -0.5f && player.isJumpPressed > 0.5f;

        // Si la combinación está pulsada y se puede activar, inicia el ciclo
        if (canTrigger && comboHeld)
        {
            canTrigger = false; // bloquea nuevas activaciones
            counter = maxCounter;
            tilemapCollider.enabled = false;
        }

        // Cuenta atrás del tiempo de desactivación
        if (counter > 0)
        {
            counter -= Time.deltaTime;
            if (counter <= 0f)
            {
                tilemapCollider.enabled = true;
                counter = 0f;
            }
        }

        // Cuando el jugador suelta la combinación, vuelve a permitir activación
        if (!comboHeld && counter <= 0f)
        {
            canTrigger = true;
        }
    }
}
