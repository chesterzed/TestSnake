using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private List<GameObject> _objectPool = new List<GameObject>();
    private Player _player;
    private Spawner _spawner;
    private UIController _UIController;

    private float _timer;

    public bool FeverModeIsActive = false;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _spawner = FindObjectOfType<Spawner>();
        _UIController = FindObjectOfType<UIController>();

        foreach (var h in FindObjectsOfType<Bonus>(true))
            _objectPool.Add(h.gameObject);
        foreach (var h in FindObjectsOfType<GroupOfPeople>(true))
            _objectPool.Add(h.gameObject);
        foreach (var h in FindObjectsOfType<Tail>(true))
            _objectPool.Add(h.gameObject);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 40f)
        {
            ResetLevel();
            _UIController.ShowGameMenu(true, _UIController.WinMenu);
        }
    }

    private void HideAll()
    {
        foreach (GameObject g in _objectPool)
            g.SetActive(false);
    }

    public void ResetLevel()
    {
        _timer = 0;
        _spawner.Reset();
        _player.Reset();
        HideAll();
        Time.timeScale = 1f;
    }
}
