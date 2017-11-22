using UnityEngine;

public class ArmsEmptyTargettingTopping : ArmsState {
    // Animator animator;
    Camera mainCamera;
    Transform holdingArea;
    Transform topping;

    public ArmsEmptyTargettingTopping(Arms _arms, Transform _topping) : base(_arms) {

        topping = _topping;

    }

    public override void OnEnter() {
        // animator = arms.animator;
        mainCamera = arms.mainCamera;
        holdingArea = arms.holdingArea;
    }

    public override void Tick() {

        RaycastHit objectInfo;
        int layerMask = 1 << 10;

        bool hitSomething = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out objectInfo, 10f, layerMask);

        if (!hitSomething) {
            arms.SetState(new ArmsEmptyState(arms));
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            LiftObject();
        }

        if (hitSomething && objectInfo.transform.tag == "Dough") {
            Transform dough = objectInfo.transform;
            arms.SetState(new ArmsHoldingIngredientOverDoughState(arms, topping, dough));
            return;
        }

    }

    public override void OnExit() {

    }

    void LiftObject() {

        //      arms.heldObject = target;

        topping.gameObject.GetComponent<Rigidbody>().useGravity = false;
        topping.gameObject.GetComponent<BoxCollider>().enabled = false;
        topping.SetParent(holdingArea);
        topping.localPosition = Vector3.zero;

        arms.SetState(new ArmsHoldingToppingState(arms, topping));
    }

}
