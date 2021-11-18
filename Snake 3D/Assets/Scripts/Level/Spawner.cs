using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] private GroupOfPeople _people;
    [SerializeField] private Bonus _bonusObj;

    [SerializeField] private float _maxSpawnDelay = 2;
    [SerializeField] private int _itemBeforeSwitchPool = 6;
    [SerializeField] private int _minItemBeforeSwitchPool = 10;
    [SerializeField] private int _maxItemBeforeSwitchPool = 18;
    
    private ColorManager _manager;
    private int _itemCounter;
    private int _objNumber;
    private float _randomPos;
    private float _elapsedTime;
    private float _spawnDelay = 0;
    private string[] _spawnObjName = { "People", "Bonus" };

    private void Awake()
    {
        _manager = FindObjectOfType<ColorManager>();

        Initialize(_people.gameObject, "People");
        Initialize(_bonusObj.gameObject, "Bonus", 3);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        _randomPos = Random.Range(-1f, 1f);

        if (_elapsedTime >= _spawnDelay)
        {
            _elapsedTime = 0;
            _spawnDelay = Random.Range(0.3f, _maxSpawnDelay);

            if (_itemCounter >= _itemBeforeSwitchPool)
            {
                _objNumber = (_objNumber + 1) % _spawnObjName.Length;
                _itemCounter = 0;

                if (_spawnObjName[_objNumber] == "Bonus")
                {
                    _itemBeforeSwitchPool = 1;
                    _spawnDelay = 5f;
                    _randomPos = 0;
                }
                else
                {
                    _itemBeforeSwitchPool = Random.Range(_minItemBeforeSwitchPool, _maxItemBeforeSwitchPool);
                }
            }

            _itemCounter++;

            if (TryGetObject(out GameObject obj, _spawnObjName[_objNumber]))
                SetObject(obj, transform.position + new Vector3(_randomPos, 0, 0));
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
}
