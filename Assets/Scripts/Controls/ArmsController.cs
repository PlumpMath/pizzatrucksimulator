using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArmsController : MonoBehaviour {
	public bool controlsEnabled = false;

	public Transform armsHolder;
	public Transform leftArm;
	public Transform rightArm;
	public Transform leftForearm;
	public Transform rightForearm;
	public Transform leftHand;
    public Transform rightHand;

	public Camera mainCamera;

	public Transform holdingArea;

	bool isHolding = false;
	Transform heldObject;
	Transform heldObjectParent;
	Animator animator;

	Vector3 initialArmsHolderPosition;
	Quaternion initialArmsHolderRotation;
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
		initialArmsHolderPosition = armsHolder.position;
		initialArmsHolderRotation = armsHolder.rotation;
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

		if (Input.GetMouseButtonDown(0)) {
			if (isHolding) {
				DropObject();
			} else  {
                LiftObject();
			}
			animator.SetTrigger("Lift");
		}
	}

	void LiftObject() {
		RaycastHit hitInfo;
		int layerMask = 1 << 10; // Layer 10, ingredients
		bool hitSomething = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, 10f, layerMask);
		Color color = hitSomething ? Color.green : Color.red;

        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * 10f, color);
        if (hitSomething) {
			isHolding = true;

			heldObject = hitInfo.transform;
			heldObject.gameObject.GetComponent<Rigidbody>().useGravity = false;
			heldObject.gameObject.GetComponent<BoxCollider>().isTrigger = true;

			heldObjectParent = heldObject.parent;
			heldObject.parent.SetParent(holdingArea);
			heldObject.parent.localPosition = Vector3.zero;
			heldObject.localPosition = Vector3.zero;

			armsHolder.localPosition = new Vector3(armsHolder.localPosition.x, armsHolder.localPosition.y, armsHolder.localPosition.z + .5f);
        }
	}
	void DropObject() {
        heldObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
        heldObject.gameObject.GetComponent<BoxCollider>().isTrigger = false;

        heldObject.parent.parent = null;
		heldObject.parent.position = holdingArea.position;
		heldObject.parent.rotation = Quaternion.identity;

        armsHolder.localPosition = new Vector3(armsHolder.localPosition.x, armsHolder.localPosition.y, armsHolder.localPosition.z - .5f);

        isHolding = false;
        heldObject = null;
    }
}
