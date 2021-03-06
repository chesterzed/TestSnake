using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EndLine : MonoBehaviour
{
    private ColorManager _manager;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _manager = FindObjectOfType<ColorManager>();
    }

    private void OnEnable()
    {
        _manager.NewColors();
        _meshRenderer.material = _manager.CurrentMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Mouth>(out Mouth mouth))
            mouth.GetComponentInParent<Player>().SetColor();
    }
}
