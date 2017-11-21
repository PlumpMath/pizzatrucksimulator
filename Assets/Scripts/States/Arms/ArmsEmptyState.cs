using UnityEngine;

public class ArmsEmptyState : ArmsState {
    Animator animator;
    Camera mainCamera;
    RaycastHit hitInfo;
    Transform holdingArea;

    public ArmsEmptyState(Arms arms) : base(arms) {

    }

    public override void OnEnter() {
        animator = arms.animator;
        mainCamera = arms.mainCamera;
        holdingArea = arms.holdingArea;
    }

    public override void Tick() {
        if (Input.GetMouseButtonDown(0)) {
            InteractWithObject();
            animator.SetTrigger("Lift");
        }
    }

    public override void OnExit() {

    }

    void InteractWithObject() {
        if (ObjectIsTargeted(out hitInfo)) {
            LiftObject();
            // hitInfo.transform.gameObject.GetComponent<IArmsClickable>().OnArmsClick(arms);
            arms.SetState(new ArmsHoldingState(arms));
        }
    }

    bool ObjectIsTargeted(out RaycastHit hitInfo) {
        int layerMask = 1 << 10; // Layer 10, ingredients

        // An object is targeted if a Ray from our main camera
        // hits it within 10 units
        bool objectIsTargeted = Physics.Raycast(
            mainCamera.transform.position,
            mainCamera.transform.forward,
            out hitInfo,
            10f,
            layerMask
        );

        // Draw our ray in the Unity editor
        Debug.DrawRay(
            mainCamera.transform.position,
            mainCamera.transform.forward * 10f,
            objectIsTargeted ? Color.green : Color.red
        );

        return objectIsTargeted;
    }

    void LiftObject() {
        Transform target = hitInfo.transform;

        GameObject pizzaObject;

        target.gameObject.GetComponent<Rigidbody>().useGravity = false;
        target.gameObject.GetComponent<BoxCollider>().enabled = false;
        target.SetParent(holdingArea);
        target.localPosition = Vector3.zero;
    }

    bool IsTargetingOven() {
        bool _isTargetingOven = hitInfo.transform != null && hitInfo.transform.gameObject.tag == "Oven";
        if (_isTargetingOven) {
            Debug.Log("That's the oven, mane.  Put a pizza in thurr.");
        }
        return _isTargetingOven;
    }
}
