using System.Collections;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    [SerializeField] float rayDistance = 5f;
    [SerializeField] float shakeAmount = 0.05f;
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float rayOffset = 1f;

    private Rigidbody2D rb;
    private Vector3 originalPosition;
    private bool activated = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
    }

    void Update()
    {
        if (activated) return;
        Vector2 rayOrigen =  new Vector2(transform.position.x, transform.position.y - rayOffset);
        RaycastHit2D rayHit = Physics2D.Raycast(rayOrigen, Vector2.down, rayDistance);
        Debug.DrawRay(rayOrigen, Vector2.down * rayDistance, Color.green);

        if (rayHit.collider != null && rayHit.transform.CompareTag("Player"))
        {
            Debug.Log("Inside If");
            activated = true;
            StartCoroutine(ShakeAndFall());
        }
    }

    IEnumerator ShakeAndFall()
    {
        float elapsed = 0f;

        // Vibración
        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-shakeAmount, shakeAmount);
            float offsetY = Random.Range(-shakeAmount, shakeAmount);

            transform.position = originalPosition + new Vector3(offsetX, offsetY, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Aseguramos que vuelve a su posición original
        transform.position = originalPosition;

        // Caída
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
