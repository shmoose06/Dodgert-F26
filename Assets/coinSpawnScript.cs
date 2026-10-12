using UnityEngine;

public class coinSpawnScript : MonoBehaviour
{
    public GameObject coin;
    public float spawnRate = 10f;

    [SerializeField] private Transform[] spawnPoints;

    private float timer = 0f;

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnCoin();
            timer = 0f;
        }
    }

    void spawnCoin()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(
            coin,
            spawnPoints[randomIndex].position,
            Quaternion.identity
        );
    }
}