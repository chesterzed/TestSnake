using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _crystals;

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
}
