using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject logPrefab;
    [SerializeField] private GameObject stonePrefab;

    [Header("Timing")]
    [SerializeField] private float spawnInterval = 2f;

    [Header("Spawn Area")]
    [SerializeField] private Vector2 areaSize = new Vector2(5f, 5f);

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= spawnInterval) {
            _timer = 0f;
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject prefabToSpawn = Random.value < 0.5f ? logPrefab : stonePrefab;
        if (prefabToSpawn == null) return;

        Vector2 randomOffset = new Vector2(
            Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
            Random.Range(-areaSize.y / 2f, areaSize.y / 2f)
        );

        Vector3 spawnPos = transform.position + (Vector3)randomOffset;
        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(areaSize.x, areaSize.y, 0f));
    }
#endif
}