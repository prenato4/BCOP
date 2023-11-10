using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atackan : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BossR boss = animator.GetComponent<BossR>();
        if (boss != null)
        {
            boss.SpawnFire();
        }
    }
}
