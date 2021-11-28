using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Man : MonoBehaviour
{
    [SerializeField] private int _points = 1;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInParent<Animator>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Mouth mouth))
            mouth.GetComponentInParent<Player>().AddScore(_points, GetComponentInChildren<MeshRenderer>().material.name);
        if (mouth != null) 
            StartCoroutine(Die(mouth.transform.position.x, mouth.Duration));
    }

    private IEnumerator Die(float endValue, float duration)
    {
        transform.DOMoveX(endValue, duration);

        _animator.Play("ManDeath");

        yield return new WaitForSeconds(0.25f);

        gameObject.SetActive(false);

    }
}
