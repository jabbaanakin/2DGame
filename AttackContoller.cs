﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackContoller : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool _isAttack;

    public bool IsAttack { get => _isAttack; }

    public void FinishAttack()
    {
        _isAttack = false;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _isAttack = true;
            animator.SetTrigger("Attack");
        }
    }
}
