using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private int _damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
            player.ApplyDamage(_damage);

        Die();
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
