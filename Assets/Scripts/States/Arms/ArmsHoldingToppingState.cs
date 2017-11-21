using UnityEngine;

public class ArmsHoldingToppingState : ArmsState {

    Transform holdingArea;
    Transform target;

    public ArmsHoldingToppingState(Arms _arms, Transform _target) : base(_arms) {

        holdingArea = arms.holdingArea;
        target = _target;

    }

    public override void OnEnter() {
    }

    public override void Tick() {

        if (Input.GetMouseButtonDown(0)) {

            DropObject();
        }

    }

    public override void OnExit() {

    }

    void DropObject() {

        target.gameObject.GetComponent<Rigidbody>().useGravity = true;
        target.gameObject.GetComponent<BoxCollider>().enabled = true;
        target.SetParent(null);
        target.position = holdingArea.position;
        target.rotation = Quaternion.Euler(-90, 0, 0);

        arms.heldObject = null;
        
        arms.SetState(new ArmsEmptyState(arms));

    }

    ArmsState GetStateFromTag(string tag) {

        if (tag == "Dough") {
            
            return new ArmsHoldingToppingOverDough(arms, target);
          }
            else {
            return null;
        }
    }
    
}
