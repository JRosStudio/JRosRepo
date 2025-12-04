using UnityEngine;
using System.Collections;

public class PlayerPlatformDrop : MonoBehaviour
{
    [Header("Platform Drop Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private float dropDuration = 0.2f;

    [Header("Rope Settings")]
    [SerializeField] private float ropeGrabVerticalThreshold = -0.5f; // Pulsar hacia abajo
    [SerializeField] private string ropeTag = "Rope";

    [Header("Referencias")]
    [SerializeField] private Player player; // Tu script del jugador

    private Collider2D playerCollider;



    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        HandlePlatformDrop();
        HandleRopeGrab();
    }

    /// <summary>
    /// Baja a través de la plataforma si pulsa ↓ + salto.
    /// </summary>
    void HandlePlatformDrop()
    {
        if (player.vertical < -0.5f && player.isJumpPressed > 0.5f)
        {
            Collider2D platformCollider = Physics2D.OverlapCircle(
                groundCheck.position,
                groundCheckRadius,
                platformLayer
            );

            if (platformCollider != null)
            {
                StartCoroutine(DisablePlatformCollision(platformCollider));
            }
        }
    }

    /// <summary>
    /// Agarrarse a la cuerda solo al pulsar ↓ (sin salto).
    /// Detectamos por TAG.
    /// </summary>
    void HandleRopeGrab()
    {
        if (player.inRope) return; 

        bool pressDown = player.vertical < ropeGrabVerticalThreshold;

        if (pressDown)
        {
            // Detectamos todo sin filtrar por capa
            Collider2D []ropeCollider = Physics2D.OverlapCircleAll(
                groundCheck.position,
                groundCheckRadius
            );

            foreach (Collider2D n in ropeCollider) {
                if (n.name == "Jumper") {
                    player.inRope = true;
                    // Quitamos gravedad y centramos al jugador en la cuerda
                    player.rb.gravityScale = 0f;
                    player.ropesHashSet.Add(n.gameObject);
                    player.lastRopeRail = n.transform.GetChild(0).position;
                    transform.position = new Vector3(
                        n.transform.position.x,
                        n.transform.position.y,
                        n.transform.position.z
                    );

                }
            }

            /*  if (ropeCollider != null && ropeCollider.CompareTag(ropeTag))
              {
                  inRope = true;

                  // Quitamos gravedad y centramos al jugador en la cuerda
                  player.rb.gravityScale = 0f;
                  transform.position = new Vector3(
                      ropeCollider.transform.position.x,
                      transform.position.y,
                      transform.position.z
                  );

                  Debug.Log("Agarrado a la cuerda: " + ropeCollider.name);
              }*/
        }
    }

    IEnumerator DisablePlatformCollision(Collider2D platformCollider)
    {
        Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
        yield return new WaitForSeconds(dropDuration);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
