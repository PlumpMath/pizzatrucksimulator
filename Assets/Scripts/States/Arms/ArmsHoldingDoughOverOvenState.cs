using UnityEngine;

public class ArmsHoldingDoughOverOvenState : ArmsState {
    Pizza pizza;
    Transform dough;
    Transform ingredient;
    Transform oven;

    public ArmsHoldingDoughOverOvenState(Arms _arms, Transform _dough, Transform _oven) : base(_arms) {
        dough = _dough;
        oven = _oven;
        pizza = dough.GetComponent<Pizza>();
    }

    public override void OnEnter() { }
    public override void OnExit() { }
    public override void Tick() {
        RaycastHit hitInfo;

        int layerMask = 1 << 10;
        bool hitSomething = Physics.Raycast(
            arms.mainCamera.transform.position,
            arms.mainCamera.transform.forward,
            out hitInfo,
            10f,
            layerMask
        );

        if (Input.GetMouseButtonDown(0)&&!pizza.cooked) {
            OvenConveyor ovenConveyor = oven.GetComponent<OvenConveyor>();
            ovenConveyor.CookPizza(dough.gameObject);
            arms.SetState(new ArmsEmptyState(arms));
            Debug.Log("You dropped the pizza into the oven!");
        }

        if (!hitSomething) {
            arms.SetState (new ArmsHoldingDoughState(arms, dough));
            return;
        }


    }
}