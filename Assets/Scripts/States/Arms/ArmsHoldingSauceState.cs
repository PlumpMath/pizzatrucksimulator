using UnityEngine;

public class ArmsHoldingSauceState : ArmsState {
    Animator animator;
    RaycastHit objectInfo;
    Transform holdingArea;
    Transform sauce;
    bool hitSomething = false;

    public ArmsHoldingSauceState(Arms _arms, Transform _sauce) : base(_arms) {
        holdingArea = arms.holdingArea;
        sauce = _sauce;
    }

    public override void OnEnter() {


    }

    public override void Tick() {
        int layerMask = 1 << 10;
        hitSomething = Physics.Raycast(
            arms.mainCamera.transform.position,
            arms.mainCamera.transform.forward,
            out objectInfo,
            10f,
            layerMask
        );

        if (hitSomething && objectInfo.transform.tag == "Dough") {
            Transform dough = objectInfo.transform;
            arms.SetState(new ArmsHoldingIngredientOverDoughState(arms, sauce, dough));
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            DropObject();
        }

    }

    public override void OnExit() {
    }

    void DropObject() {
        sauce.gameObject.GetComponent<Rigidbody>().useGravity = true;
        sauce.gameObject.GetComponent<BoxCollider>().enabled = true;
        sauce.SetParent(null);
        sauce.position = holdingArea.position;
        sauce.rotation = Quaternion.Euler(-90, 0, 0);
        arms.heldObject = null;
        arms.SetState(new ArmsEmptyState(arms));
    }
}
