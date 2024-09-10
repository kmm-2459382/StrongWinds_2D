using UnityEngine;

public class DogController : MonoBehaviour
{
    public float speed = 2f;
    public float jumpForce = 5f;
    public float jumpIntervalMin = 1f;
    public float jumpIntervalMax = 3f;
    private Rigidbody2D rb;
    private float nextJumpTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextJumpTime = Time.time + Random.Range(jumpIntervalMin, jumpIntervalMax);
    }

    void Update()
    {
        // ������E�ֈړ�
        rb.velocity = new Vector2(speed, rb.velocity.y);

        // �����_���ȃ^�C�~���O�ŃW�����v
        if (Time.time >= nextJumpTime)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            nextJumpTime = Time.time + Random.Range(jumpIntervalMin, jumpIntervalMax);
        }

        // ��ʊO�ɏo����폜
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
}
