using UnityEngine;

public class ArmsHoldingToppingOverDough : ArmsState {

    Animator animator;
    Camera mainCamera;
    Transform holdingArea;
    Transform target;
    Pizza pizza;

    public ArmsHoldingToppingOverDough(Arms _arms, Transform _target) : base(_arms) {

        holdingArea = arms.holdingArea;
        target = _target;

    }

    public override void OnEnter() {
    }

    public override void Tick() {

        if (Input.GetMouseButtonDown(0)) {

            LayerToppings();
        }

    }

    public override void OnExit() {

    }

    void LayerToppings() {

	    pizza = target.GetComponent<Pizza>();
        if (pizza.doughRolled == true && pizza.sauceAdded == true && pizza.cheeseAdded == true)
        {
             pizza.ingredientsList.Add(target.GetComponent<Ingredient>());
             Debug.Log(("The "+ arms.heldObject + " is topped."));
       //      arms.heldObject = null;
             arms.SetState(new ArmsEmptyState(arms));
        }
    }

}
