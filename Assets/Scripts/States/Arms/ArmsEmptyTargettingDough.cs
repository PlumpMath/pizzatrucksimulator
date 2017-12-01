using UnityEngine;
using UnityEngine.UI;

public class ArmsEmptyTargettingDough : ArmsState {
    // Animator animator;
    Camera mainCamera;
    Transform holdingArea;
    Transform dough;
    Pizza pizza;

    bool rollingDough;

    public ArmsEmptyTargettingDough(Arms _arms, Transform _dough) : base(_arms) {
        dough = _dough;
    }

    public override void OnEnter() {
        // animator = arms.animator;
        mainCamera = arms.mainCamera;
        holdingArea = arms.holdingArea;
        pizza = dough.GetComponent<Pizza>();

        pizza.label.GetComponent<Text>().enabled = true;

        if (pizza.cooked == true) {
            pizza.label.transform.gameObject.SetActive(true);
            pizza.label.GetComponent<Text>().enabled = true;

            pizza.label.GetComponent<Text>().text = "Serve the pizza!";
        }

    }

    public override void Tick() {

        RaycastHit objectInfo;
        int layerMask = 1 << 10;

        bool hitSomething = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out objectInfo, 10f, layerMask);

        if (!hitSomething) {
            arms.SetState(new ArmsEmptyState(arms));
            return;
        }

        if (Input.GetMouseButtonDown(0) && !pizza.rollingDough) {
            if (pizza.doughRolled) {
                LiftObject();
            } else {
                pizza.RollDough();
            }
            return;
        }

        if (hitSomething && objectInfo.transform.tag == "Sauce") {
            arms.SetState(new ArmsEmptyTargettingSauce(arms, objectInfo.transform));
            return;
        }

                if (hitSomething && objectInfo.transform.tag == "Cheese") {
            arms.SetState(new ArmsEmptyTargettingCheese(arms, objectInfo.transform));
            return;
        }

                        if (hitSomething && objectInfo.transform.tag == "Toppings") {
            arms.SetState(new ArmsEmptyTargettingTopping(arms, objectInfo.transform));
            return;
        }
    }


    public override void OnExit() {

        pizza.label.GetComponent<Text>().enabled = false;


    }

    void LiftObject() {
        Arms.Instance.GetComponent<AudioSource>().clip = Arms.Instance.AudioClipPickUp;
        Arms.Instance.GetComponent<AudioSource>().Play();
        Arms.Instance.animator.SetTrigger("Lift");
        dough.gameObject.GetComponent<Rigidbody>().useGravity = false;
        dough.gameObject.GetComponent<BoxCollider>().enabled = false;
        dough.SetParent(holdingArea);
        dough.localPosition = Vector3.zero;
        dough.localRotation = Quaternion.Euler(-120, 0, 0);
        if (!pizza.cooked) {
            arms.SetState(new ArmsHoldingDoughState(arms, dough));
        } else if (pizza.cooked) {
            arms.SetState(new ArmsHoldingCookedPizzaState(arms, dough));
        }
    }

}
