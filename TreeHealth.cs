using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float totalHealth = 100f;

    private float _health;
    private TreeController _treeController;

    private void Start()
    {
        _health = totalHealth;
        _treeController = GetComponent<TreeController>();
        InitHealth();
    }
    public void ReduceHealth(float damage)
    {
        _health -= damage;
        InitHealth();

        if (_treeController.IsTurnedRight && _treeController.DistanceToPlayer() < 0)
            _treeController.Flip();

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
