using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float totalHealth = 100f;

    private float _health;
    private Animator _animator;
    private EnemyController _enemyController;

    private void Start()
    {
        _health = totalHealth;
        _animator = GetComponent<Animator>();
        _enemyController = GetComponent<EnemyController>();
        InitHealth();
    }
    public void ReduceHealth(float damage)
    {
        _health -= damage;
        InitHealth();

        if (_enemyController.IsTurnedRight && _enemyController.DistanceToPlayer() < 0)
            _enemyController.Flip();

        _animator.SetTrigger("TakeDamage");

        if (_health <= 0f)
            Die();
    }

    private void InitHealth()
    {
        healthSlider.value = _health / totalHealth;
    }
    private void Die()
    {
        gameObject.SetActive(false);
    }
}
