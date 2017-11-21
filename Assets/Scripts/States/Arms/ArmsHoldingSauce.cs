using UnityEngine;

public class ArmsHoldingSauce : ArmsState {

    Animator animator;
    RaycastHit objectInfo;
    Transform holdingArea;
    Transform target;
    bool hitSomething = false;

    public ArmsHoldingSauce(Arms _arms, Transform _target) : base(_arms) {

        holdingArea = arms.holdingArea;
        target = _target;

    }

    public override void OnEnter() {


    }

    public override void Tick() {

        string tag;
        int layerMask = 1 << 10;
        ArmsState newState;

        hitSomething = Physics.Raycast(arms.mainCamera.transform.position, arms.mainCamera.transform.forward, out objectInfo, 10f, layerMask);
        // only then check if raycast hits anything
        if (hitSomething) {
            target = objectInfo.transform;
            Debug.Log(target);
            tag = objectInfo.transform.tag;
            newState = GetStateFromTag(tag);
            if (newState != null) {
                arms.SetState(newState);
            }

        }

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
            
            return new ArmsHoldingSauceOverDough(arms, target);
          }
        else {
            return null;
        }
    }
    

}
