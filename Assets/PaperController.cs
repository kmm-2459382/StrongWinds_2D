using UnityEngine;

public class PaperController : MonoBehaviour
{
    public float speed = 2f;          // ‰¡ˆÚ“®‚Ì‘¬“x
    public float frequency = 2f;      // ã‰º‚Ì”g‚ÌŽü”g”
    public float magnitude = 0.5f;    // ã‰º‚Ì”g‚ÌU•
    private Vector3 startPosition;    // ‰ŠúˆÊ’u
    private float elapsedTime = 0f;   // Œo‰ßŽžŠÔ

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Œo‰ßŽžŠÔ‚ðXV
        elapsedTime += Time.deltaTime;

        // ã‰º‚É”g‘Å‚¿‚È‚ª‚çˆÚ“®‚·‚é
        Vector3 newPosition = startPosition;
        newPosition.x += speed * elapsedTime;
        newPosition.y += Mathf.Sin(elapsedTime * frequency) * magnitude;
        transform.position = newPosition;

        // ‰æ–ÊŠO‚Éo‚½‚çíœ
        if (transform.position.x > Camera.main.orthographicSize * Camera.main.aspect)
        {
            Destroy(gameObject);
        }
    }
}
