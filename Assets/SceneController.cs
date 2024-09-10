using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public AudioClip gameStartBGM;
    public AudioClip gameBGM;
    public AudioClip gameOverBGM;
    public AudioClip gameClearBGM;

    private AudioSource audioSource;

    void Start()
    {
        audioSource.loop = true; // ループ設定
        audioSource = GetComponent<AudioSource>();

        // 初回シーンのBGMを再生
        PlayBGM(gameStartBGM);
        ChangeScene("GameStart Scene", gameStartBGM);

        // Game Sceneの場合は30秒後にチェック
        if (SceneManager.GetActiveScene().name == "Game Scene")
        {
            Invoke(nameof(CheckGameClear), 30f); // デリゲートを使用してメソッドを呼び出す
            ChangeScene("GameClear Scene", gameClearBGM);
        }
    }

    void Update()
    {
        // 画面タップでシーン遷移
        if (Input.GetMouseButtonDown(0))
        {
            string sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == "GameStart Scene")
            {
                ChangeScene("Game Scene", gameBGM);
            }
            else if (sceneName == "GameOver Scene" || sceneName == "GameClear Scene")
            {
                ChangeScene("GameStart Scene", gameStartBGM);
            }
        }
    }

    void CheckGameClear()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null && player.CurrentHealth > 0)
        {
            ChangeScene("GameClear Scene", gameClearBGM);
        }
        else
        {
            ChangeScene("GameOver Scene", gameOverBGM);
        }
    }

    public void ChangeScene(string sceneName, AudioClip bgm)
    {
        SceneManager.LoadScene(sceneName);
        PlayBGM(bgm);
    }

    void PlayBGM(AudioClip bgm)
    {
        if (audioSource.clip != bgm)
        {
            audioSource.Stop();
            audioSource.clip = bgm;
            audioSource.Play();
        }
    }
}
