using UnityEngine;

public class RopeTipJumper : MonoBehaviour
{
    Player player;

    [SerializeField] Transform tpPoint;
    [SerializeField] float groundCheckDistance = 0.2f;
    [SerializeField] LayerMask platformMask; // Capas de plataformas válidas

    public bool jumpEnabled = false;

    private void Awake()
    {
        player = FindFirstObjectByType<Player>();
    }

    // Llama a esto cuando la cuerda TERMINA de colocarse
    public void EvaluateAnchorPoint()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, groundCheckDistance, platformMask);
        Debug.Log(hit);
        jumpEnabled = hit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("PlayerFeet")) return;
        if (!player.inRope) return;
        if (!jumpEnabled) return;

        ExitRopeAtTop();
    }

    private void ExitRopeAtTop()
    {
        player.inRope = false;
        player.transform.position = tpPoint.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = jumpEnabled ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * groundCheckDistance);
    }
}
