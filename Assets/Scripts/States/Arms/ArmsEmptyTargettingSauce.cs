using UnityEngine;
using UnityEngine.UI;

public class ArmsEmptyTargettingSauce : ArmsState {
    // Animator animator;
    Camera mainCamera;
    Transform holdingArea;
    Transform sauce;
    Pizza pizza;

    public ArmsEmptyTargettingSauce(Arms _arms, Transform _sauce) : base(_arms) {
        sauce = _sauce;
    }

    public override void OnEnter() {
        // animator = arms.animator;
        mainCamera = arms.mainCamera;
        holdingArea = arms.holdingArea;
                sauce.GetComponent<Ingredient>().label.GetComponent<Text>().enabled =true;

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
        }

    }

    public override void OnExit() {
                sauce.GetComponent<Ingredient>().label.GetComponent<Text>().enabled =false;

    }

    void LiftObject() {
           Arms.Instance.GetComponent<AudioSource>().clip = Arms.Instance.AudioClipPickUp;
        Arms.Instance.GetComponent<AudioSource>().Play();
        Arms.Instance.animator.SetTrigger("Lift");
        sauce.gameObject.GetComponent<Rigidbody>().useGravity = false;
        sauce.gameObject.GetComponent<BoxCollider>().enabled = false;
        sauce.SetParent(holdingArea);
        sauce.localPosition = Vector3.zero;
        arms.SetState(new ArmsHoldingSauceState(arms, sauce));
    }
}
