using UnityEngine;

public class ObjectLayer : ScriptableObject
{
    [SerializeField] private GameObject _layerPrefab;

    public GameObject LayerPrefab => _layerPrefab;
}