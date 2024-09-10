using UnityEngine;

public class PaperController : MonoBehaviour
{
    public float speed = 2f;          // ���ړ��̑��x
    public float frequency = 2f;      // �㉺�̔g�̎��g��
    public float magnitude = 0.5f;    // �㉺�̔g�̐U��
    private Vector3 startPosition;    // �����ʒu
    private float elapsedTime = 0f;   // �o�ߎ���

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // �o�ߎ��Ԃ��X�V
        elapsedTime += Time.deltaTime;

        // �㉺�ɔg�ł��Ȃ���ړ�����
        Vector3 newPosition = startPosition;
        newPosition.x += speed * elapsedTime;
        newPosition.y += Mathf.Sin(elapsedTime * frequency) * magnitude;
        transform.position = newPosition;

        // ��ʊO�ɏo����폜
        if (transform.position.x > Camera.main.orthographicSize * Camera.main.aspect)
        {
            Destroy(gameObject);
        }
    }
}
