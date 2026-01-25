using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] float rayDistance = 0.5f;
    [SerializeField] float shakeAmount = 0.05f;
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float rayOffset = 1f;
    [SerializeField] GameObject sprite;
    [SerializeField] GameObject bottomDamage;

    [SerializeField] LayerMask groundLayer;
    private bool falling = false;

    private Rigidbody2D rb;
    private Vector3 spriteOriginalLocalPos;
    private bool activated = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteOriginalLocalPos = sprite.transform.localPosition;
    }

    void Update()
    {

       
     //Destroy(gameObject);

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player")) {
           StartCoroutine(ShakeAndFall());
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (!falling) return;

        if (((1 << col.gameObject.layer) & groundLayer )!= 0 )
        {
            Destroy(gameObject);
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

            sprite.transform.localPosition = spriteOriginalLocalPos + new Vector3(offsetX, offsetY, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Aseguramos que vuelve a su posición original
        sprite.transform.localPosition = spriteOriginalLocalPos;

        // Caída
        rb.bodyType = RigidbodyType2D.Dynamic;
        bottomDamage.SetActive(true);
        falling = true;
        activated = true;
    }
}
