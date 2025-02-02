using UnityEngine;

public class PaperController : MonoBehaviour
{
    public float speed = 2f;          // 横移動の速度
    public float frequency = 2f;      // 上下の波の周波数
    public float magnitude = 0.5f;    // 上下の波の振幅
    private Vector3 startPosition;    // 初期位置
    private float elapsedTime = 0f;   // 経過時間

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // 経過時間を更新
        elapsedTime += Time.deltaTime;

        // 上下に波打ちながら移動する
        Vector3 newPosition = startPosition;
        newPosition.x += speed * elapsedTime;
        newPosition.y += Mathf.Sin(elapsedTime * frequency) * magnitude;
        transform.position = newPosition;

        // 画面外に出たら削除
        if (transform.position.x > Camera.main.orthographicSize * Camera.main.aspect)
        {
            Destroy(gameObject);
        }
    }
}
