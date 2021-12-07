using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class Player : ObjectPool
{
    [SerializeField] private SpawnerSetup spwnr;
    [SerializeField] private Tail _tail;
    [SerializeField] private float _cubeEdgeSize;
    [SerializeField] private int _score = 0;
    private int _crystals;

    private GameManager _gameManager;
    private UIController _UIController;
    private MeshRenderer _meshRenderer;
    private ColorManager _colorManager;
    private PlayerMover _mover;

    private List<Transform> tiles = new List<Transform>();


    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _UIController = FindObjectOfType<UIController>();
        _colorManager = FindObjectOfType<ColorManager>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _mover = GetComponent<PlayerMover>();

        Initialize(_tail.gameObject, "Tail");
    }

    private void OnEnable()
    {
        StartCoroutine(FeverMode(2, 4));
    }

    private void Start()
    {
        SetColor();

        _UIController.ScoreUpdate(_score);
        _UIController.CrystalUpdate(_crystals);

        AddTile();
    }

    private void Update()
    {
        float PosX;
        
        if (tiles.Count > 0)
        {
            PosX = Mathf.Lerp(tiles[0].position.x, transform.position.x, Time.deltaTime * _mover.PlayerSpeed);
            tiles[0].position = new Vector3(PosX, _mover.PlayerPos.y, _mover.PlayerPos.z - _cubeEdgeSize);
            tiles[0].LookAt(transform, Vector3.up);
        }
        if (tiles.Count > 1)
        {
            for (int i = 0; i < tiles.Count - 1; i++)
            {
                PosX = Mathf.Lerp(tiles[i + 1].position.x, tiles[i].position.x, Time.deltaTime * _mover.PlayerSpeed);
                tiles[i + 1].position = new Vector3(PosX, _mover.PlayerPos.y, _mover.PlayerPos.z - _cubeEdgeSize * (i + 2));
                tiles[i + 1].LookAt(tiles[i], Vector3.up);
            }
        }
    }

    public void AddScore(int points, string matName)
    {
        if (_meshRenderer.material.name == matName || _gameManager.FeverModeIsActive)
        {
            _score += points;
            AddTile();
        }
        else
        {
            Die();
        }
        _UIController.ScoreUpdate(_score);
    }

    public void AddCrystal(int cryctalCount)
    {
        _crystals++;

        if (_crystals >= 3)
        {
            _crystals = 0;
            StartCoroutine(FeverMode(5, 3));
        }

        _UIController.CrystalUpdate(_crystals);

    }

    public void SetColor()
    {
        _meshRenderer.material = _colorManager.CurrentMaterial;

        foreach (var t in tiles)
            t.gameObject.GetComponent<MeshRenderer>().material = _meshRenderer.material;
    }

    public void Die()
    {
        if (PlayerPrefs.GetInt("bestScore", 0) > _score)
            PlayerPrefs.SetInt("bestScore", _score);

        gameObject.SetActive(false);

        _UIController.ShowGameMenu(true, _UIController.LooseMenu);
    }

    public void Reset()
    {
        _score = 0;
        _crystals = 0;
        spwnr.MultiplySpeedK = 1;
        gameObject.SetActive(true);
        tiles.Clear();

        _colorManager.NewColors();
        _UIController.ScoreUpdate(_score);
        _UIController.CrystalUpdate(_crystals);

        SetColor();

        StartCoroutine(FeverMode(2, 4));
    }

    private void AddTile()
    {
        if (TryGetObject(out GameObject obj, "Tail"))
        {
            SetTail(obj, new Vector3(transform.position.x, _mover.PlayerPos.y, _mover.PlayerPos.z - _cubeEdgeSize * (tiles.Count)));
            tiles.Add(obj.transform);
        }

        foreach (var t in tiles)
            t.gameObject.GetComponent<MeshRenderer>().material = _meshRenderer.material;
    }

    private void SetTail(GameObject obj, Vector3 spawnPoint)
    {
        obj.SetActive(true);
        obj.transform.position = spawnPoint;
    }

    private IEnumerator<WaitForSeconds> FeverMode(float time, int newSpeed)
    {
        spwnr.MultiplySpeedK = 3;
        _gameManager.FeverModeIsActive = true;
        transform.position = _mover.PlayerPos;
        transform.rotation = Quaternion.identity;

        yield return new WaitForSeconds(time);

        spwnr.MultiplySpeedK = 1;
        _gameManager.FeverModeIsActive = false;
    }
}
