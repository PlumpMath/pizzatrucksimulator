using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CharacterAnimator : MonoBehaviour {


    public const float locomotionAnimationSmoothTime = .1f;

    NavMeshAgent agent;
    Animator animator;
    public float speedPercent;

    public Rigidbody[] ragdollRigidbodies;
    // Use this for initialization
    void Start() {

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update() {
        speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime); // set speed
    }
}
