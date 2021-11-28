using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _crystals;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _looseMenu;
    [SerializeField] private GameManager _gameManager;

    public GameObject LooseMenu => _looseMenu;
    public GameObject WinMenu => _winMenu;

    public void ScoreUpdate(string text)
    {
        _score.text = text;
    }

    public void ScoreUpdate(int scoreCount)
    {
        _score.text = "Score: \n" + scoreCount.ToString();
    }

    public void CrystalUpdate(int crystals)
    {
        _crystals.text = crystals.ToString();
    }

    public void ShowGameMenu(bool isActive, GameObject menu)
    {
        if (isActive)
        {
            menu.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (!isActive)
        {
            menu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Restart()
    {
        ShowGameMenu(false, _looseMenu);
        ShowGameMenu(false, _winMenu);
        _gameManager.ResetLevel();
    }
}
