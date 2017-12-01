using UnityEngine;

public class ArmsHoldingDoughState : ArmsState {
    Animator animator;
    RaycastHit objectInfo;
    Transform holdingArea;
    Transform dough;
    bool hitSomething = false;

    public ArmsHoldingDoughState(Arms _arms, Transform _dough) : base(_arms) {
        holdingArea = arms.holdingArea;
        dough = _dough;
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

        if (hitSomething && objectInfo.transform.tag == "Oven") {
            Transform oven = objectInfo.transform;
            arms.SetState(new ArmsHoldingDoughOverOvenState(arms, dough, oven));
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            DropObject();
        }

    }

    public override void OnExit() {
    }

    void DropObject() {
                Arms.Instance.GetComponent<AudioSource>().clip = Arms.Instance.AudioClipDrop;
        Arms.Instance.GetComponent<AudioSource>().Play();
                Arms.Instance.animator.SetTrigger("Drop");
        dough.gameObject.GetComponent<Rigidbody>().useGravity = true;
        dough.gameObject.GetComponent<BoxCollider>().enabled = true;
        dough.SetParent(null);
        dough.position = holdingArea.position;
        dough.rotation = Quaternion.Euler(-90, 0, 0);
        arms.heldObject = null;
        arms.SetState(new ArmsEmptyState(arms));
    }
}
