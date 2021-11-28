using UnityEngine;
using DG.Tweening;

public class Mine : MonoBehaviour
{
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Mouth>(out Mouth mouth) && !_gameManager.FeverModeIsActive)
            mouth.GetComponentInParent<Player>().Die();

        Die();
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
