using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private MaterialPool _matPool;

    private int _currentMaterialNumber;
    private int _secondMaterialNumber;

    private int _lastNumber;

    public Material CurrentMaterial => _matPool.Materials[_currentMaterialNumber];
    public Material SecondMaterial => _matPool.Materials[_secondMaterialNumber];

    public void NewColors()
    {
        _lastNumber = _currentMaterialNumber;
        while (_currentMaterialNumber == _lastNumber)
            _currentMaterialNumber = Random.Range(0, _matPool.Materials.Length);

        _lastNumber = _secondMaterialNumber;
        while (_secondMaterialNumber == _lastNumber || _secondMaterialNumber == _currentMaterialNumber)
            _secondMaterialNumber = Random.Range(0, _matPool.Materials.Length);
    }
}
