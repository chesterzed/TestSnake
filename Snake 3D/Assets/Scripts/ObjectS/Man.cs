using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : MonoBehaviour
{
    [SerializeField] private int _points = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            player.AddScore(_points);

        Die();
    }


    private void Die()
    {
        gameObject.SetActive(false);
    }
}
