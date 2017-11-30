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
	void Start () {

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {

        speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime); // set speed
		
	}

	public void MakeRagdoll()
	{
        AudioSource audio = GetComponent<AudioSource>();
        audio.pitch = Random.Range(.6f, 1.4f);
        audio.Play();

		foreach (Rigidbody ragdollRigidbody in ragdollRigidbodies)

		{

       
			ragdollRigidbody.isKinematic = false;
			ragdollRigidbody.mass = .05f;
			ragdollRigidbody.drag = 0f;
			ragdollRigidbody.angularDrag = 0f;
		}
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<CapsuleCollider>().enabled = false;

		animator.enabled = false;
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.enabled = false;
		



		
	}

}
