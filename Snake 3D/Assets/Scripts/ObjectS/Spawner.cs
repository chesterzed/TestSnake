using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] private GameObject[] _objLayer;

    [SerializeField] private float _spawnTimeDelay = 2;
    
    private float _elapsedTime;

    private void Start()
    {
        Initialize(_objLayer[]);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _spawnTimeDelay)
        {
            _elapsedTime = 0;
            Instantiate(_objLayer, transform.position, Quaternion.identity);
        }
    }

    private void SetEnemy(GameObject enemy, Vector3 spawnPoint)
    {
        enemy.SetActive(true);
        enemy.transform.position = spawnPoint;
    }
}
