using UnityEngine;

public class ArmsHoldingSauceOverDough : ArmsState {

    Animator animator;
    Transform holdingArea;
    Transform target;
    Pizza pizza;

    public ArmsHoldingSauceOverDough(Arms _arms, Transform _target) : base(_arms) {

        holdingArea = arms.holdingArea;
        target = _target;

    }

    public override void OnEnter() {


    }

  public override void Tick() {

        RaycastHit objectInfo;
        int layerMask = 1 << 10;

        bool hitSomething = Physics.Raycast(arms.mainCamera.transform.position, arms.mainCamera.transform.forward, out objectInfo, 10f, layerMask);

        if (!hitSomething) {
            arms.SetState (new ArmsHoldingSauce(arms, target));
            return;
        }

        if (Input.GetMouseButtonDown(0)){
            
            LayerSauce();
            
        }

    }

    public override void OnExit() {

    }


        public void LayerSauce()
    {
	    pizza = target.GetComponent<Pizza>();
        if (pizza.doughRolled == true)
        {
            pizza.sauceAdded = true;
             pizza.ingredientsList.Add(target.GetComponent<Ingredient>());
             Debug.Log("The cheese is sprlinkled.");
        //     arms.heldObject = null;
             arms.SetState(new ArmsEmptyState(arms));
        }
    }


}
