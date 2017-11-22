using UnityEngine;

public class ArmsHoldingToppingState : ArmsState {

    Transform holdingArea;
    Transform topping;

    public ArmsHoldingToppingState(Arms _arms, Transform _topping) : base(_arms) {
        holdingArea = arms.holdingArea;
        topping = _topping;
    }

    public override void OnEnter() {
    }

    public override void Tick() {
        int layerMask = 1 << 10;
        RaycastHit hitInfo;
        bool hitSomething = Physics.Raycast(
            arms.mainCamera.transform.position,
            arms.mainCamera.transform.forward,
            out hitInfo,
            10f,
            layerMask
        );

        if (hitSomething && hitInfo.transform.tag == "Dough") {
            arms.SetState(new ArmsHoldingIngredientOverDoughState(arms, topping, hitInfo.transform));
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            DropObject();
        }
    }

    public override void OnExit() {

    }

    void DropObject() {
        topping.gameObject.GetComponent<Rigidbody>().useGravity = true;
        topping.gameObject.GetComponent<BoxCollider>().enabled = true;
        topping.SetParent(null);
        topping.position = holdingArea.position;
        topping.rotation = Quaternion.Euler(-90, 0, 0);

        arms.heldObject = null;

        arms.SetState(new ArmsEmptyState(arms));

    }
}
