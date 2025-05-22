using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static SpawnManager Instance;
    spawnpoint[] spawnPoints;
    private void Awake()
    {
        Instance = this;
        spawnPoints = GetComponentsInChildren<spawnpoint>();
    }

    public Transform GetSpawnpoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
