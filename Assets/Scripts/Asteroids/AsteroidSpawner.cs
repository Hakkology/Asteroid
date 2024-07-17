using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public static AsteroidSpawner Instance { get; private set; }
    public Asteroid asteroidPrefab;
    public float spawnRate = 2.0f;
    public float spawnDistance = 15.0f;
    public float spawnDelay = 2.0f;
    public float trajectoryVariance = 15.0f;
    public int spawnAmount = 3;

    void Awake() {
        Instance = this;
    }
    void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), spawnDelay, spawnRate);
    }
 
    private void SpawnAsteroid()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPoint = transform.position + spawnDirection;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion spawnRotation = Quaternion.AngleAxis(variance, Vector3.forward);

           new AsteroidBuilder(asteroidPrefab)
                .InstantiateAsteroid(spawnPoint, spawnRotation)
                .SetSize(Random.Range(asteroidPrefab.minSize, asteroidPrefab.maxSize))
                .SetTrajectory(spawnRotation * -spawnDirection)
                .Build();
        }
    }
}
