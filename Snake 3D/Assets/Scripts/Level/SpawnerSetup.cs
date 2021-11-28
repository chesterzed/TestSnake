using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerSetup", menuName = "CreateSpawnerSetup/SpawnerSetup", order = 51)]
public class SpawnerSetup : ScriptableObject
{
    [SerializeField] private GroupOfPeople _people;
    [SerializeField] private Bonus _bonus;

    [SerializeField] private float _peopleSpawnRadius;
    [SerializeField] private float _enemySpeed;
    [SerializeField] private float _minSpawnDistance = 2;
    [SerializeField] private float _maxSpawnDistance = 2;
    [SerializeField] private int _minItemBeforeSwitchPool = 10;
    [SerializeField] private int _maxItemBeforeSwitchPool = 18;

    public float MultiplySpeedK = 1;

    public GroupOfPeople PeoplePrefab => _people;
    public Bonus BonusPrefab => _bonus;

    public float PeopleSpawnRadius => _peopleSpawnRadius;
    public float EnemySpeed => _enemySpeed;
    public float ModEnemySpeed => _enemySpeed * MultiplySpeedK;
    public float MinSpawnDistance => _minSpawnDistance;
    public float MaxSpawnDistance => _maxSpawnDistance;
    public int MinItemBeforeSwitchPool => _minItemBeforeSwitchPool;
    public int MaxItemBeforeSwitchPool => _maxItemBeforeSwitchPool;
}
