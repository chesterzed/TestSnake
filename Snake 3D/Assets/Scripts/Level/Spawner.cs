using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] private SpawnerSetup spwnr;
    
    private ColorManager _manager;
    private int _itemBeforeSwitchPool;
    private int _itemCounter;
    private int _objNumber;
    private float _randomPos;
    private float _elapsedTime;
    private float _spawnDelay = 0;
    private string[] _spawnObjName = { "People", "Bonus" };

    private void Awake()
    {
        _manager = FindObjectOfType<ColorManager>();

        Initialize(spwnr.PeoplePrefab.gameObject, "People");
        Initialize(spwnr.BonusPrefab.gameObject, "Bonus", 1);
    }

    private void Start()
    {
        _itemBeforeSwitchPool = Random.Range(spwnr.MinItemBeforeSwitchPool, spwnr.MaxItemBeforeSwitchPool);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        _randomPos = Random.Range(-1f, 1f);

        if (_elapsedTime >= _spawnDelay)
        {
            _elapsedTime = 0;
            _spawnDelay = Random.Range(spwnr.MinSpawnDistance / spwnr.ModEnemySpeed, spwnr.MaxSpawnDistance / spwnr.ModEnemySpeed);

            if (_itemCounter >= _itemBeforeSwitchPool)
            {
                _objNumber = (_objNumber + 1) % _spawnObjName.Length;
                _itemCounter = 0;

                if (_spawnObjName[_objNumber] == "Bonus")
                {
                    _itemBeforeSwitchPool = 1;
                    _spawnDelay = 9f / spwnr.EnemySpeed;
                    _randomPos = 0;
                }
                else
                {
                    _itemBeforeSwitchPool = Random.Range(spwnr.MinItemBeforeSwitchPool, spwnr.MaxItemBeforeSwitchPool);
                }
            }

            _itemCounter++;

            if (TryGetObject(out GameObject obj, _spawnObjName[_objNumber]))
                SetObject(obj, new Vector3(transform.position.x + _randomPos, transform.position.y, transform.position.z));
        }
    }

    private void SetObject(GameObject obj, Vector3 spawnPoint)
    {
        if (obj.TryGetComponent<GroupOfPeople>(out GroupOfPeople gop))
            if (_itemCounter % 2 == 0)
                gop.CurrentSpawnMaterial = _manager.CurrentMaterial;
            else if (_itemCounter % 2 == 1)
                gop.CurrentSpawnMaterial = _manager.SecondMaterial;

        obj.SetActive(true);
        obj.transform.position = spawnPoint;
    }

    public void Reset()
    {
        _spawnDelay = 0;
        _objNumber = 0;
        _itemCounter = 0;
        _itemBeforeSwitchPool = Random.Range(spwnr.MinItemBeforeSwitchPool, spwnr.MaxItemBeforeSwitchPool);
    }
}
