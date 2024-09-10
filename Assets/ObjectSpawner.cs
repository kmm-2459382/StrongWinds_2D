using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject dogPrefab;
    public GameObject cabbagePrefab;
    public GameObject paperPrefab;
    public float spawnIntervalMin = 1f;
    public float spawnIntervalMax = 3f;
    public float screenBottom = -4.5f;
    public float screenTop = 4.5f;

    void Start()
    {
        // èââÒÇÃÉXÉ|Å[ÉìÇê›íË
        Invoke("SpawnDog", Random.Range(spawnIntervalMin, spawnIntervalMax));
        Invoke("SpawnCabbage", Random.Range(spawnIntervalMin, spawnIntervalMax));
        Invoke("SpawnPaper", Random.Range(spawnIntervalMin, spawnIntervalMax));
    }

    void SpawnDog()
    {
        Vector3 spawnPosition = new Vector3(-Camera.main.orthographicSize * Camera.main.aspect - 1, screenBottom, 0);
        Instantiate(dogPrefab, spawnPosition, Quaternion.identity);
        Invoke("SpawnDog", Random.Range(spawnIntervalMin, spawnIntervalMax));
    }

    void SpawnCabbage()
    {
        Vector3 spawnPosition = new Vector3(-Camera.main.orthographicSize * Camera.main.aspect - 1, Random.Range(screenBottom, screenTop), 0);
        Instantiate(cabbagePrefab, spawnPosition, Quaternion.identity);
        Invoke("SpawnCabbage", Random.Range(spawnIntervalMin, spawnIntervalMax));
    }

    void SpawnPaper()
    {
        Vector3 spawnPosition = new Vector3(-Camera.main.orthographicSize * Camera.main.aspect - 1, Random.Range(screenBottom, screenTop), 0);
        Instantiate(paperPrefab, spawnPosition, Quaternion.identity);
        Invoke("SpawnPaper", Random.Range(spawnIntervalMin, spawnIntervalMax));
    }
}
