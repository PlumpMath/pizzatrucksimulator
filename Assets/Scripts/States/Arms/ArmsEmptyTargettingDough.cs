using UnityEngine;

public class ArmsEmptyTargettingDough : ArmsState {
    // Animator animator;
    Camera mainCamera;
    Transform holdingArea;
    Transform dough;
    Pizza pizza;

    public ArmsEmptyTargettingDough(Arms _arms, Transform _dough) : base(_arms) {
        dough = _dough;
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
            arms.SetState (new ArmsEmptyState(arms));
            return;
        }

        if (Input.GetMouseButtonDown(0)){
			pizza = dough.GetComponent<Pizza>();
            pizza.RollDough();
        }
    }

    
    public override void OnExit() {

    }

    void LiftObject() {
        dough.gameObject.GetComponent<Rigidbody>().useGravity = false;
        dough.gameObject.GetComponent<BoxCollider>().enabled = false;
        dough.SetParent(holdingArea);
        dough.localPosition = Vector3.zero;
    }

}
