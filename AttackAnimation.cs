using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AttackContoller attackContoller = animator.transform.root.GetComponent<AttackContoller>();
        attackContoller.FinishAttack();
    }
}
