using UnityEngine;
using System.Collections;

public class PlayerPlatformDrop : MonoBehaviour
{
    [Header("Platform Drop")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.5f, 0.1f);
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private float dropDuration = 0.2f;

    [Header("Rope Settings")]
    [SerializeField] private float ropeGrabVerticalThreshold = -0.5f;
    [SerializeField] private string ropeTag = "Rope";

    [Header("Referencias")]
    [SerializeField] private Player player;

    private Collider2D playerCollider;
    private bool dropping = false;

    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        HandlePlatformDrop();
        HandleRopeGrab();
    }

    // ------------------------------------------------------------
    // PLATAFORMAS ONE-WAY + DOBLE DETECCIÓN IZQ / DER
    // ------------------------------------------------------------
    void HandlePlatformDrop()
    {
        if (dropping) return;

        // Combinación típica de bajar + salto
        if (player.vertical < -0.5f && player.isJumpPressed > 0.5f)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(
                groundCheck.position,
                groundCheckSize,
                0f,
                platformLayer
            );

            if (hits.Length > 0)
            {
                // --- NUEVO: si encuentra dos plataformas, cae por ambas ---
                if (hits.Length == 2)
                {
                    StartCoroutine(DisableTwoPlatforms(hits[0], hits[1]));
                    return;
                }

                // Comportamiento normal: cae sólo una
                Collider2D topPlatform = hits[0];
                float highestY = topPlatform.bounds.max.y;

                for (int i = 1; i < hits.Length; i++)
                {
                    if (hits[i].bounds.max.y > highestY)
                    {
                        highestY = hits[i].bounds.max.y;
                        topPlatform = hits[i];
                    }
                }

                StartCoroutine(DisablePlatformCollision(topPlatform));
            }
        }
    }

    // Cae a través de una plataforma
    IEnumerator DisablePlatformCollision(Collider2D platformCollider)
    {
        dropping = true;

        Physics2D.IgnoreCollision(playerCollider, platformCollider, true);

        yield return new WaitForSeconds(dropDuration);

        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);

        yield return null;
        dropping = false;
    }

    // CAER POR DOS PLATAFORMAS A LA VEZ
    IEnumerator DisableTwoPlatforms(Collider2D p1, Collider2D p2)
    {
        dropping = true;

        Physics2D.IgnoreCollision(playerCollider, p1, true);
        Physics2D.IgnoreCollision(playerCollider, p2, true);

        yield return new WaitForSeconds(dropDuration);

        Physics2D.IgnoreCollision(playerCollider, p1, false);
        Physics2D.IgnoreCollision(playerCollider, p2, false);

        yield return null;
        dropping = false;
    }

    // ------------------------------------------------------------
    // CUERDAS
    // ------------------------------------------------------------
    void HandleRopeGrab()
    {
        if (player.inRope) return;

        bool pressDown = player.vertical < ropeGrabVerticalThreshold;
        if (!pressDown) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(groundCheck.position, 0.3f);

        foreach (Collider2D c in hits)
        {
            if (c.CompareTag(ropeTag) || c.name == "Jumper")
            {
                GrabRope(c);
                return;
            }
        }
    }

    private void GrabRope(Collider2D ropeCollider)
    {
        player.inRope = true;
        player.rb.gravityScale = 0f;

        player.ropesHashSet.Add(ropeCollider.gameObject);
        player.lastRopeRail = ropeCollider.transform.GetChild(0).position;

        transform.position = new Vector3(
            ropeCollider.transform.position.x,
            ropeCollider.transform.position.y,
            transform.position.z
        );
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        }
    }
}
