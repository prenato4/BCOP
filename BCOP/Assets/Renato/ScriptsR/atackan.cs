using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atackan : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Boss boss = animator.GetComponent<Boss>();
        if (boss != null)
        {
            boss.SpawnFire();
        }
    }
}
