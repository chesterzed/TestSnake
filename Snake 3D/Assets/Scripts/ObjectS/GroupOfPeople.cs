using UnityEngine;

public class GroupOfPeople : MonoBehaviour
{
    [SerializeField] private SpawnerSetup spwnr;

    private Man[] _group;
    private Animator[] _animator;

    public Material CurrentSpawnMaterial;

    private void Awake()
    {
        _group = GetComponentsInChildren<Man>();
        _animator = GetComponentsInChildren<Animator>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _group.Length; i++)
        {
            _group[i].gameObject.SetActive(true);
            _group[i].gameObject.GetComponentInChildren<MeshRenderer>().material = CurrentSpawnMaterial;
            _group[i].transform.localPosition = new Vector3(Random.Range(-spwnr.PeopleSpawnRadius, spwnr.PeopleSpawnRadius), 0, Random.Range(-spwnr.PeopleSpawnRadius, spwnr.PeopleSpawnRadius));
            _group[i].transform.localScale = new Vector3(1, 1, 1);

            _animator[i].Play("ManIdle");
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.back * spwnr.ModEnemySpeed * Time.deltaTime);
    }
}
