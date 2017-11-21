using UnityEngine;

public class ArmsHoldingCheese : ArmsState {

    Animator animator;
    RaycastHit hitInfo;
    Transform holdingArea;
    Transform cheese;
    Transform dough;

    public ArmsHoldingCheese(Arms _arms, Transform _target) : base(_arms) {

        holdingArea = arms.holdingArea;
        cheese = _target;

    }

    public override void OnEnter() {

    }

    public override void Tick() {


        string tag;
        RaycastHit objectInfo;
        int layerMask = 1 << 10;
        ArmsState newState;

        bool hitSomething = Physics.Raycast(arms.mainCamera.transform.position, arms.mainCamera.transform.forward, out objectInfo, 10f, layerMask);
        // only then check if raycast hits anything
        if (hitSomething) {
            dough = objectInfo.transform;
            tag = objectInfo.transform.tag;
        
            if (tag == "Dough")
            {
                arms.SetState(new ArmsHoldingCheeseOverDough(arms, cheese, dough));
                return;
            }
            

        }

        if (Input.GetMouseButtonDown(0)) {

            DropObject();
        }

    }

    public override void OnExit() {

    }

    void DropObject() {



        cheese.gameObject.GetComponent<Rigidbody>().useGravity = true;
        cheese.gameObject.GetComponent<BoxCollider>().enabled = true;
        cheese.SetParent(null);
        cheese.position = holdingArea.position;
        cheese.rotation = Quaternion.Euler(-90, 0, 0);
        arms.heldObject = null;
        arms.SetState(new ArmsEmptyState(arms));

    }
    


}
