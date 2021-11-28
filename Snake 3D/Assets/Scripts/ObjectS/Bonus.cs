using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private SpawnerSetup spwnr;

    private Crystal[] _childsCr;
    private Mine[] _childsMi;
    private EndLine _end;

    private void Awake()
    {
        _childsCr = GetComponentsInChildren<Crystal>();
        _childsMi = GetComponentsInChildren<Mine>();
        _end = GetComponentInChildren<EndLine>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _childsCr.Length; i++)
            _childsCr[i].gameObject.SetActive(true);

        for (int i = 0; i < _childsMi.Length; i++)
            _childsMi[i].gameObject.SetActive(true);

        _end.gameObject.SetActive(true);
    }

    private void Update()
    {
        transform.Translate(Vector3.back * spwnr.ModEnemySpeed * Time.deltaTime);
    }
}
