using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _size = 2;

    private MeshRenderer _meshRenderer;
    private ColorManager _colorManager;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _colorManager = FindObjectOfType<ColorManager>(); 
    }

    private void Start()
    {
        SwitchColor();
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
        }
        else
        {
            _size -= (points * 5);
        }
    }

    public void AddCrystal(int cryctalCount)
    {

    }

    public void SwitchColor()
    {
        _meshRenderer.material = _colorManager.CurrentMaterial;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
