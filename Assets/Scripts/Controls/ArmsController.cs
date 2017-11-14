using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ArmsController : MonoBehaviour {
	public bool controlsEnabled = false;

	public Transform leftArm;
	public Transform rightArm;
	public Transform leftForearm;
	public Transform rightForearm;
	public Transform leftHand;
    public Transform rightHand;

	Animator animator;

	// Vector3 initialLeftForearmScale;
	// Vector3 initialRightForearmScale;
	// Quaternion initialLeftHandRotation;
	// Quaternion initialRightHandRotation;

	#region Singleton
	public static ArmsController Instance;


	void Awake() {
        if (Instance != null) {
            Debug.LogWarning("More than one ArmsController instance!");
            return;
        }
        Instance = this;
		this.animator = this.GetComponent<Animator>();
	}
	#endregion Singleton

	void Start () {
		// initialLeftForearmScale = leftForearm.localScale;
		// initialRightForearmScale = rightForearm.localScale;
		// initialLeftHandRotation = leftHand.rotation;
		// initialRightHandRotation = rightHand.rotation;
	}
	
	void Update () {
		if (!controlsEnabled) {
			return;
		}
		// float speed = 5f;
		if (Input.GetMouseButtonDown(0)) {
			animator.SetTrigger("Lift");
			// leftHand.Rotate(0, 0, -1 * speed);
			// leftForearm.localScale = new Vector3(leftForearm.localScale.x * 1.05f, leftForearm.localScale.y, leftForearm.localScale.z);
			// leftForearm.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.back * 5000f, ForceMode.VelocityChange);
			// leftArm.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.back * 5000f, ForceMode.VelocityChange);
		} else {// if (leftHand.rotation != initialLeftHandRotation) {
			// leftHand.rotation = Quaternion.RotateTowards(leftHand.rotation, initialLeftHandRotation, 0.75f * speed);
			// leftHand.rotation = initialLeftHandRotation;
		}
	}
}
