using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] private int _points = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Mouth>(out Mouth mouth))
            mouth.GetComponentInParent<Player>().AddCrystal(_points);

        Die();
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
