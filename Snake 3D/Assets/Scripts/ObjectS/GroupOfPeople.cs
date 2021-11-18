using UnityEngine;

public class GroupOfPeople : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Man[] _group;

    public Material CurrentSpawnMaterial;

    private void Awake()
    {
        _group = GetComponentsInChildren<Man>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _group.Length; i++)
        {
            _group[i].gameObject.SetActive(true);
            _group[i].gameObject.GetComponent<MeshRenderer>().material = CurrentSpawnMaterial;
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.back * _speed * Time.deltaTime);
    }
}
