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
 
        if (Input.GetMouseButtonDown(0)) {
            DropObject();
        }
        
        if (hitSomething && hitInfo.transform.tag == "Dough") {
            arms.SetState(new ArmsHoldingIngredientOverDoughState(arms, topping, hitInfo.transform));
            return;
        }


    }

    public override void OnExit() {

    }

    void DropObject() {
        Arms.Instance.GetComponent<AudioSource>().clip = Arms.Instance.AudioClipDrop;
        Arms.Instance.GetComponent<AudioSource>().Play();
        Arms.Instance.animator.SetTrigger("Drop");

        topping.gameObject.GetComponent<Rigidbody>().useGravity = true;
        topping.gameObject.GetComponent<BoxCollider>().enabled = true;
        topping.SetParent(null);
        topping.position = holdingArea.position;
        topping.rotation = Quaternion.Euler(-90, 0, 0);

        arms.heldObject = null;

        arms.SetState(new ArmsEmptyState(arms));

    }
}
