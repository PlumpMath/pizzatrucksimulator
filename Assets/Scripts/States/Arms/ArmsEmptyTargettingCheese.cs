using UnityEngine;
using UnityEngine.UI;

public class ArmsEmptyTargettingCheese : ArmsState {
    // Animator animator;
    Camera mainCamera;
    Transform holdingArea;
    Transform cheese;
    Pizza pizza;

    public ArmsEmptyTargettingCheese(Arms _arms, Transform _cheese) : base(_arms) {
        cheese = _cheese;
    }

    public override void OnEnter() {
        // animator = arms.animator;
        mainCamera = arms.mainCamera;
        holdingArea = arms.holdingArea;
        cheese.GetComponent<Ingredient>().label.GetComponent<Text>().enabled =true;

    }

    public override void Tick() {

        RaycastHit objectInfo;
        int layerMask = 1 << 10;

        bool hitSomething = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out objectInfo, 10f, layerMask);

        if (hitSomething)
        {
        Debug.Log(objectInfo.transform.name);
        }

        if (!hitSomething) {
            arms.SetState (new ArmsEmptyState(arms));
            return;
        }

        if (Input.GetMouseButtonDown(0)){
                LiftObject();
                return;
        }

        if (hitSomething && objectInfo.transform.tag == "Toppings")
        {
            arms.SetState(new ArmsEmptyTargettingTopping(arms, objectInfo.transform));
            return;
        }

    }

    public override void OnExit() {

                cheese.GetComponent<Ingredient>().label.GetComponent<Text>().enabled =false;


    }

        void LiftObject() {
            
       // arms.heldObject = target;
        Arms.Instance.GetComponent<AudioSource>().clip = Arms.Instance.AudioClipPickUp;
        Arms.Instance.GetComponent<AudioSource>().Play();
        Arms.Instance.animator.SetTrigger("Lift");
        GameManager.Instance.DelayedReplenish(cheese);
        cheese.gameObject.GetComponent<Rigidbody>().useGravity = false;
        cheese.gameObject.GetComponent<BoxCollider>().enabled = false;
        cheese.SetParent(holdingArea);
        cheese.localPosition = Vector3.zero;

        arms.SetState(new ArmsHoldingCheeseState(arms, cheese));
    }

}
