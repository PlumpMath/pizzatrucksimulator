using UnityEngine;
using UnityEngine.UI;

public class ArmsEmptyTargettingTopping : ArmsState {
    // Animator animator;
    Camera mainCamera;
    Transform holdingArea;
    Transform topping;
     string s;
 //   GameObject targetTopping;

    public ArmsEmptyTargettingTopping(Arms _arms, Transform _topping) : base(_arms) {

        topping = _topping;

    }

    public override void OnEnter() {

        mainCamera = arms.mainCamera;
        holdingArea = arms.holdingArea;
        topping.GetComponent<Ingredient>().label.GetComponent<Text>().enabled =true;
    //    targetTopping = topping.gameObject;
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
            return;
        }

        if (hitSomething && objectInfo.transform.tag == "Toppings")
        {
            arms.SetState(new ArmsEmptyTargettingTopping(arms, objectInfo.transform));
            return;
        }

       if (hitSomething && objectInfo.transform.tag == "Cheese")
        {
            arms.SetState(new ArmsEmptyTargettingCheese(arms, objectInfo.transform));
            return;
        }

        if (hitSomething && objectInfo.transform.tag == "Dough") {
            Transform dough = objectInfo.transform;
            arms.SetState(new ArmsHoldingIngredientOverDoughState(arms, topping, dough));
            return;
        }

    }

    public override void OnExit() {
        topping.GetComponent<Ingredient>().label.GetComponent<Text>().enabled =false;
    }

    void LiftObject() {
        Arms.Instance.GetComponent<AudioSource>().clip = Arms.Instance.AudioClipPickUp;
        Arms.Instance.GetComponent<AudioSource>().Play();
        //      arms.heldObject = target;
        Arms.Instance.animator.SetTrigger("Lift");
        GameManager.Instance.DelayedReplenish(topping);
        topping.gameObject.GetComponent<Rigidbody>().useGravity = false;
        topping.gameObject.GetComponent<BoxCollider>().enabled = false;
        topping.SetParent(holdingArea);
        topping.localPosition = Vector3.zero;

        arms.SetState(new ArmsHoldingToppingState(arms, topping));
    }

}
