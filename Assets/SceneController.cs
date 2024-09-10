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
        audioSource.loop = true; // ���[�v�ݒ�
        audioSource = GetComponent<AudioSource>();

        // ����V�[����BGM���Đ�
        PlayBGM(gameStartBGM);
        ChangeScene("GameStart Scene", gameStartBGM);

        // Game Scene�̏ꍇ��30�b��Ƀ`�F�b�N
        if (SceneManager.GetActiveScene().name == "Game Scene")
        {
            Invoke(nameof(CheckGameClear), 30f); // �f���Q�[�g���g�p���ă��\�b�h���Ăяo��
            ChangeScene("GameClear Scene", gameClearBGM);
        }
    }

    void Update()
    {
        // ��ʃ^�b�v�ŃV�[���J��
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
