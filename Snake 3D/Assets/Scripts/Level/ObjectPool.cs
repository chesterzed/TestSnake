using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    private List<GameObject> _poolPeople = new List<GameObject>();
    private List<GameObject> _poolBonus = new List<GameObject>();
    private List<GameObject> _poolTail = new List<GameObject>();

    protected void Initialize(GameObject prefab, string type)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(prefab, _container.transform);
            spawned.SetActive(false);

            switch (type)
            {
                case "People":
                    _poolPeople.Add(spawned);
                    break;
                case "Bonus":
                    _poolBonus.Add(spawned);
                    break;
                case "Tail":
                    _poolTail.Add(spawned);
                    break;
            }
        }
    }

    protected void Initialize(GameObject prefab, string type, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject spawned = Instantiate(prefab, _container.transform);
            spawned.SetActive(false);

            switch (type)
            {
                case "People":
                    _poolPeople.Add(spawned);
                    break;
                case "Bonus":
                    _poolBonus.Add(spawned);
                    break;
                case "Tail":
                    _poolTail.Add(spawned);
                    break;
            }
        }
    }

    protected bool TryGetObject(out GameObject result, string type)
    {
        switch (type)
        {
            case "People":
                result = _poolPeople.FirstOrDefault(p => p.activeSelf == false);
                break;
            case "Bonus":
                result = _poolBonus.FirstOrDefault(p => p.activeSelf == false);
                break;
            case "Tail":
                result = _poolTail.FirstOrDefault(p => p.activeSelf == false);
                break;
            default:
                result = null;
                break;
        }
        
        return result != null;
    }

}
