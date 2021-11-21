using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class Player : ObjectPool
{
    [SerializeField] private UIController _UIController;
    [SerializeField] private GameObject _tail;
    [SerializeField] private int _health;
    [SerializeField] float _cubeEdgeSize;
    [SerializeField] private int _size = 2;
    private int _crystals;

    private MeshRenderer _meshRenderer;
    private ColorManager _colorManager;
    private PlayerMover _mover;

    private List<Transform> tiles = new List<Transform>();

    private void Awake()
    {
        _UIController = FindObjectOfType<UIController>();
        _colorManager = FindObjectOfType<ColorManager>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _mover = GetComponent<PlayerMover>();

        Initialize(_tail, "Tail");
    }

    private void Start()
    {
        SetColor();

        _crystals = PlayerPrefs.GetInt("crystals");

        _UIController.ScoreUpdate(_size);
        _UIController.CrystalUpdate(_crystals);

        for (int i = 0; i < _size; i++)
            AddTile();
    }

    private void Update()
    {
        float PosX;
        
        if (tiles.Count > 0)
        {
            PosX = Mathf.Lerp(tiles[0].position.x, transform.position.x, Time.deltaTime * _mover.Speed);
            tiles[0].position = new Vector3(PosX, _mover.PlayerPos.y, _mover.PlayerPos.z - _cubeEdgeSize);
        }
        if (tiles.Count > 1)
        {
            for (int i = 0; i < tiles.Count - 1; i++)
            {
                PosX = Mathf.Lerp(tiles[i + 1].position.x, tiles[i].position.x, Time.deltaTime * _mover.Speed);
                tiles[i + 1].position = new Vector3(PosX, _mover.PlayerPos.y, _mover.PlayerPos.z - _cubeEdgeSize * (i + 2));
            }
        }
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    public void AddScore(int points, string matName)
    {
        if (_meshRenderer.material.name == matName)
        {
            _size += points;
            AddTile();
        }
        else
        {
            if ((points * 5) > tiles.Count)
                points = tiles.Count;
            else
                points *= 5;

            _size -= points;

            for (int i = 0; i < points; i++)
                RemoveTile();
        }
        _UIController.ScoreUpdate(_size);
    }

    public void AddCrystal(int cryctalCount)
    {
        _crystals++;
        PlayerPrefs.SetInt("crystals", _crystals);
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
        gameObject.SetActive(false);
     
        if (PlayerPrefs.GetInt("bestScore", 0) > _size)
            PlayerPrefs.SetInt("bestScore", _size);
    }

    private void AddTile()
    {
        if (TryGetObject(out GameObject obj, "Tail"))
        {
            SetTail(obj, new Vector3(transform.position.x, _mover.PlayerPos.y, _mover.PlayerPos.z - _cubeEdgeSize * (tiles.Count + 1)));
            tiles.Add(obj.transform);
        }

        foreach (var t in tiles)
            t.gameObject.GetComponent<MeshRenderer>().material = _meshRenderer.material;
    }

    private void RemoveTile()
    {
        tiles[tiles.Count - 1].gameObject.SetActive(false);
        tiles.RemoveAt(tiles.Count - 1);
    }

    private void SetTail(GameObject obj, Vector3 spawnPoint)
    {
        obj.SetActive(true);
        obj.transform.position = spawnPoint;
    }
}
