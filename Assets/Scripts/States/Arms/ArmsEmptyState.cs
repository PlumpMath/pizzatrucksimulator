using UnityEngine;

public class ArmsEmptyState : ArmsState {
    // Animator animator;
    Camera mainCamera;
    RaycastHit hitInfo;
    // Transform holdingArea;
    Transform target;

    public ArmsEmptyState(Arms arms) : base(arms) {

    }

    public override void OnEnter() {
        // animator = arms.animator;
        // holdingArea = arms.holdingArea;
        mainCamera = arms.mainCamera;
    }

    public override void Tick() {

        string tag;
        RaycastHit objectInfo;
        int layerMask = 1 << 10;
        ArmsState newState;

        bool hitSomething = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out objectInfo, 10f, layerMask);
        // only then check if raycast hits anything
        if (hitSomething) {
            target = objectInfo.transform;
            tag = objectInfo.transform.tag;
            newState = GetStateFromTag(tag);
            if (newState != null) {
                arms.SetState(newState);
            }
        }
    }

    public override void OnExit() {
    }

    ArmsState GetStateFromTag(string tag) {
        if (tag == "Toppings") {
            return new ArmsEmptyTargettingTopping(arms, target);
        }
        if (tag == "Dough") {
            return new ArmsEmptyTargettingDough(arms, target);
        }
        if (tag == "Sauce") {
            return new ArmsEmptyTargettingSauce(arms, target);
        }
        if (tag == "Cheese") {
            return new ArmsEmptyTargettingCheese(arms, target);
        }
        if (tag == "Oven") {
            return new ArmsEmptyTargettingOven(arms, target);
        } else {
            return null;
        }
    }

}