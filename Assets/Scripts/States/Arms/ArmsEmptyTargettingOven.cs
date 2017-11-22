using UnityEngine;

public class ArmsEmptyTargettingOven : ArmsState {
    // Animator animator;
    // Transform holdingArea;
    // Transform target;
    Camera mainCamera;

    public ArmsEmptyTargettingOven(Arms _arms, Transform _target) : base(_arms) {
        // target = _target;
    }

    public override void OnEnter() {
        // animator = arms.animator;
        // holdingArea = arms.holdingArea;
        mainCamera = arms.mainCamera;
    }

    public override void Tick() {

        RaycastHit objectInfo;
        int layerMask = 1 << 10;

        bool hitSomething = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out objectInfo, 10f, layerMask);

        if (!hitSomething) {
            arms.SetState (new ArmsEmptyState(arms));
            return;
        }
    }

    public override void OnExit() {

    }

}
