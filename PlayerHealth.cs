using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject healElement;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Animator _animator;
    [SerializeField] private float totalHealth = 100f;

    private bool _isHealed = false;
    private float _health;

    private void Start()
    {
        _health = totalHealth;
    }

    private void Update()
    {
        healthSlider.value = _health / totalHealth;
        if(_isHealed)
        {
            totalHealth += 50f;
            _health = totalHealth;
            _isHealed = false;
            healElement.SetActive(false);
        }
    }
    public void ReduceHealth(float damage)
    {
        _health -= damage;
        _animator.SetTrigger("TakeDamage");

        if (_health <= 0f)
            Die();
    }
    private void Die()
    {
        gameObject.SetActive(false);
        gameOverCanvas.SetActive(true);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Heal"))
        {
            _isHealed = true;
        }
    }
}
