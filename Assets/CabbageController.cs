using UnityEngine;

public class CabbageController : MonoBehaviour
{
    public float speed = 2f;
    public float bounceForce = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
    }

    void Update()
    {
        // ‰æ–ÊŠO‚Éo‚½‚çíœ
        if (transform.position.x > Camera.main.orthographicSize * Camera.main.aspect)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamage(10);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = new Vector2(speed, bounceForce);
        }
    }
}
