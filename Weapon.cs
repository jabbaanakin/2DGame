using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    private AttackContoller _attackContoller;
    private void Start()
    {
        _attackContoller = transform.root.GetComponent<AttackContoller>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth= other.GetComponent<EnemyHealth>();
        if(enemyHealth != null && _attackContoller.IsAttack)
        {
            enemyHealth.ReduceHealth(damage);
        }
    }
}
