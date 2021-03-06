﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FechadoBehaviour : StateMachineBehaviour {

    Transform playerPos;
    public float num;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (Vector2.Distance(animator.transform.position, playerPos.transform.position) <= num)
        {
            animator.SetBool("Abrindo", true);
            animator.SetBool("Fechado", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    
    }
}
