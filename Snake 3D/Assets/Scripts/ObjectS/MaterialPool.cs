using UnityEngine;

[CreateAssetMenu(fileName = "matt", menuName = "CreateMat/Material", order = 51)]
public class MaterialPool : ScriptableObject
{
    [SerializeField] private Material[] _materials;

    public Material[] Materials => _materials;
}
