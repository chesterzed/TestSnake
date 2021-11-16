using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;

    public void ApplyDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    public void AddScore(int points)
    {

    }

    public void Die()
    {

    }
}
